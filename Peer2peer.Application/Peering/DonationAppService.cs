using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.Timing;
using Peer2peer.Dto;
using Peer2peer.EntityFramework.Repositories;
using Peer2peer.Peering.Dto;
using Peer2peer.Sessions;
using Peer2peer.Users;

namespace Peer2peer.Peering
{
    public interface IDonationAppService : IApplicationService
    {
        OutputResultDto BuyPackage(BuyPackageInput input);

        OutputResultDto CreateDonationTicket(CreateDonationTicketInput input);

        OutputResultDto CreateDonation(CreateDonationInput input);

        OutputResultDto RemoveTaledDonation(RemoveTaledDonationInput input);

        OutputResultDto ChangeDonationStatus(ChangeDonationStatusInput input);

        OutputResultDto ConfirmDonation(ConfirmDonationInput input);

        PagedResultDto<PackageWithTicket> GetPackageWithTickets(PagedResultRequestDto input);

        OutputResultDto<Package> GetNextPachageToMatch();

        OutputResultDto MatchPackage(MatchPackageInput input);
    }

    public class DonationAppService: IDonationAppService
    {
        private readonly IRepository<PackageType> _packageTypeRepository;
        private readonly IRepository<Package> _packageRepository;
        private readonly IRepository<DonationTicket> _ticketRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly ISessionAppService _sessionAppService;

        public DonationAppService(IRepository<Package> packageRepository, IRepository<PackageType> packageTypeRepository
            , IRepository<DonationTicket> ticketRepository, IDonationRepository donationRepository, 
            IRepository<User, long> userRepository , ISessionAppService sessionService)
        {
            _packageTypeRepository = packageTypeRepository;
            _packageRepository = packageRepository;
            _ticketRepository = ticketRepository;
            _donationRepository = donationRepository;
            _userRepository = userRepository;
            _sessionAppService = sessionService;
        }

        [UnitOfWork(IsDisabled = true)]
        public OutputResultDto BuyPackage(BuyPackageInput input)
        {
            if (_packageRepository.GetAll().Any(p => p.UserId == input.UserId && p.Status == Status.Pending))
            {
                return new OutputResultDto {Message = "Please waiting for your current circle to be completed"};
            }
            var name = input.Type;
            var packageType = _packageTypeRepository.FirstOrDefault(p => p.Name == name.ToLower());

            if (packageType == null)
            {
                return new OutputResultDto {Message = "Invalid package selected"};
            }

            var package = new Package
            {
                UserId = input.UserId,
                ExpectedRi = packageType.ReturnOnInvestment,
                CurrentRi = 0,
                TypeId = packageType.Id,
                Status = Status.Pending,
                CreatedDate = Clock.Now
            };

            _packageRepository.Insert(package);

            //create donation for this ticket
            var packageToMatchResult = GetNextPachageToMatch();
            if (packageToMatchResult.Success)
            {
                var packageToMatch = packageToMatchResult.Data;
                MatchPackage(new MatchPackageInput {PackageId = packageToMatch.Id});
            }
            return new OutputResultDto {Message = "Package created", Success = true};
        }

        public OutputResultDto CreateDonationTicket(CreateDonationTicketInput input)
        {
            var package = _packageRepository.FirstOrDefault(p => p.Id == input.PackageId);
            if (package == null) return new OutputResultDto {Message = "Invalid Package Id"};
            //if the package have the expected number of ticket, return
            var type = _packageTypeRepository.FirstOrDefault(p => p.Id == package.TypeId);
            var expectedDonationCount = type.ReturnOnInvestment / type.Price;
            if (_ticketRepository.Count(t => t.PackageId == package.Id) >= expectedDonationCount)
            {
                return new OutputResultDto
                {
                    Message = "The selected package cannot have more than " + expectedDonationCount + " donations"
                };
            }
            for (var i = 1; i <= expectedDonationCount; i++)
            {
                _ticketRepository.Insert(new DonationTicket
                {
                    Amount = type.Price,
                    DateCreated = Clock.Now,
                    PackageId = package.Id,
                    UserId = package.UserId,
                    Status = Status.Pending
                });
            }

            package.Status = Status.TicketsCreated;

            return new OutputResultDto
            {
                Message = expectedDonationCount + " donations created for the package",
                Success = true
            };
        }

        [UnitOfWork(IsDisabled = true)]
        public OutputResultDto CreateDonation(CreateDonationInput input)
        {
            var package = _packageRepository.FirstOrDefault(p => p.Id == input.PackageId);
            if (package.Status != Status.Pending)
            {
                return new OutputResultDto {Message = "Package cannot be paired twice"};
            }
            if (_donationRepository.FirstOrDefault(p => p.Id == input.PackageId) != null)
            {
                return new OutputResultDto { Message = "Package cannot be paired twice" };
            }
            var ticket = _ticketRepository.FirstOrDefault(t => t.Id == input.TicketId);
            _donationRepository.Insert(new Donation
            {
                PackageId = input.PackageId,
                Status = Status.Pending,
                Amount = ticket.Amount,
                Date = Clock.Now,
                FromUserId = package.UserId,
                TicketId = ticket.Id,
                ToUserId = ticket.UserId
            });

            ticket.Status = Status.AwaitingPayment;
            ticket.DonationId = input.PackageId;
            _ticketRepository.Update(ticket);
            
            //mark donation as paired to avoid multi pairing of a donation
            package.Status = Status.Paired;
            _packageRepository.Update(package);
            return new OutputResultDto {Message = "Donation created for the given ticket and package", Success = true};
        }

        public OutputResultDto RemoveTaledDonation(RemoveTaledDonationInput input)
        {
            var ticket = _ticketRepository.FirstOrDefault(t => t.Id == input.TicketId);
            if (ticket == null) return new OutputResultDto {Message = "Invalid ticket ID"};
            if (!ticket.DonationId.HasValue)
            {
                return new OutputResultDto {Message = "Selected ticket has no donation"};
            }
            var donation = _donationRepository.FirstOrDefault(d => d.Id == ticket.DonationId.Value);
            var package = _packageRepository.FirstOrDefault(p => p.Id == donation.PackageId);
            //delete the donation, delete the package, suspend the account and set urgent pairing on the ticket
            _donationRepository.Delete(donation);
            _packageRepository.Delete(package);
            var user = _userRepository.FirstOrDefault(u => u.Id == package.UserId);
            user.IsActive = false;
            _userRepository.Update(user);
            ticket.Status = Status.UrgentPairingNeeded;
            _ticketRepository.Update(ticket);
            return new OutputResultDto {Message = "Donation removed", Success = true};
        }

        public OutputResultDto ChangeDonationStatus(ChangeDonationStatusInput input)
        {
            var donation =
                _donationRepository.FirstOrDefault(d => d.Id == input.DonationId && d.ToUserId == input.CurrentUserId);
            if (donation == null)
            {
                return new OutputResultDto {Message = "Invalid donation ID"};
            }
            donation.Status = input.Status;
            _donationRepository.Update(donation);
            return new OutputResultDto {Message = input.Status == Status.AwaitingAlert?
                "Donation marked as awaiting payment" : "Donation status changed", Success = true};
        }

        public OutputResultDto ConfirmDonation(ConfirmDonationInput input)
        {
            var donation = _donationRepository.FirstOrDefault(d => d.Id == input.DonationId &&
            d.ToUserId == input.CurrentUserId);
            if (donation == null)
            {
                return new OutputResultDto {Message = "Invalid Donation ID"};
            }
            donation.Status = Status.PaidOut;
            _donationRepository.Update(donation);
            var ticket = _ticketRepository.FirstOrDefault(t => t.Id == donation.TicketId);
            //mark the ticket as paid out
            ticket.Status = Status.PaidOut;
            _ticketRepository.InsertOrUpdateAndGetId(ticket);

            //if there is no other package that have not been paid out for this package, then mark the package as paid out
            if (_ticketRepository.FirstOrDefault(t => t.Id != ticket.Id && t.PackageId == ticket.PackageId && 
            t.Status != Status.PaidOut) == null)
            {
                var package = _packageRepository.Get(ticket.PackageId);
                package.Status = Status.PaidOut;
                _packageRepository.Update(package);
            }

            //create tickets for the package
            var result = CreateDonationTicket(new CreateDonationTicketInput {PackageId = donation.PackageId});
            if (!result.Success)
            {
                //todo log ticket creation error
            }
            return new OutputResultDto {Message = "Thanks for confirming the donation", Success = true};
        }

        public PagedResultDto<PackageWithTicket> GetPackageWithTickets(PagedResultRequestDto input)
        {
            var query =
                _packageRepository.GetAll()
                    .Where(p => p.Status == Status.TicketsCreated && p.Tickets.Any(t => t.Status == Status.Pending ||
                    t.Status == Status.UrgentPairingNeeded))
                    .Select(p =>
                        new PackageWithTicket
                        {
                            PackageId = p.Id,
                            Amount = p.Type.Price,
                            DateCreated = p.CreatedDate,
                            Name = p.User.Name + " " + p.User.Surname,
                            PhoneNumber = p.User.PhoneNumber
                        });
            var count = query.Count();
            var list = query.OrderBy(p=>p.DateCreated).PageBy(input).ToList();
            return new PagedResultDto<PackageWithTicket>(count, list);
        }

        public OutputResultDto<Package> GetNextPachageToMatch()
        {
            try
            {
                var package =
                _packageRepository
                    .GetAll().OrderBy(p=>p.CreatedDate)
                    .FirstOrDefault(p => p.Status == Status.TicketsCreated &&
                                         p.Tickets.Any(t => t.Status == Status.Pending ||
                                                            t.Status == Status.UrgentPairingNeeded));
                return new OutputResultDto<Package> { Data = package, Success = true };
            }
            catch (Exception exception)
            {
                return new OutputResultDto<Package> {Success = false, Message = exception.Message};
            }
            
        }

        public OutputResultDto MatchPackage(MatchPackageInput input)
        {
            var receptorPackage = _packageRepository.FirstOrDefault(p => p.Id == input.PackageId);
            if (receptorPackage == null) return new OutputResultDto {Message = "Invalid package Id"};
            var tickets = _ticketRepository.GetAllList(t => t.PackageId == input.PackageId &&
                                                            (t.Status == Status.Pending ||
                                                             t.Status == Status.UrgentPairingNeeded));
            if (tickets.Count == 0)
                return new OutputResultDto {Message = "The selected package has no pending tickets"};
          /*  var pendingPackages = _packageRepository.GetAll().
                OrderBy(p=>p.CreatedDate).Where(p => p.Status == Status.Pending).Take(2).ToList();*/
            var donationsCount = 0;
            //var i = 0;

            foreach (var ticket in tickets)
            {
                var donorPackage = _packageRepository.FirstOrDefault(p => p.Status == Status.Pending && p.TypeId == receptorPackage.TypeId);// pendingPackages[i++];
                //if the ticket has donation. continue
                if(ticket.DonationId.HasValue) continue;
                var result = CreateDonation(new CreateDonationInput {PackageId = donorPackage.Id, TicketId = ticket.Id});
                if (result.Success) donationsCount++;
            }
            return new OutputResultDto
            {
                Message = $"{donationsCount} out of {tickets.Count} donations created", Success = true
            };

        }
        
    }
}
