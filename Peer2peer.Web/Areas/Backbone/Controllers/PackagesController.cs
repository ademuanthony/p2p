using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Timing;
using Peer2peer.EntityFramework.Repositories;
using Peer2peer.Peering;
using Peer2peer.Peering.Dto;
using Peer2peer.Users;
using Peer2peer.Web.Controllers;

namespace Peer2peer.Web.Areas.Backbone.Controllers
{
    public class PackagesController : BackendControllerBase
    {
        private readonly IRepository<Package> _packageRepository;
        private readonly IRepository<PackageType> _packageTypeRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IDonationAppService _donationService;

        public PackagesController(IUserAppService userService, IRepository<User, long> userRepository,
            IRepository<Package> packageRepository, IRepository<PackageType> packageTypeRepository,
            IDonationRepository donationRepository, IDonationAppService donationService):base(userService)
        {
            _userRepository = userRepository;
            _packageRepository = packageRepository;
            _packageTypeRepository = packageTypeRepository;
            _donationRepository = donationRepository;
            _donationService = donationService;
        }

        public ActionResult Index()
        {
            return Pending();
        }

        // GET: Backbone/Pakcages
        public ActionResult Pending()
        {
            var status = Status.Pending.ToLower();
            var packages = _packageRepository.GetAll().Include(p=>p.User).Include(p=>p.Type).Where(p =>p.Status == status).ToList();
            return View(packages);
        }

        public ActionResult Confirmed(int page = 1, int count = 20)
        {
            var offset = (page - 1) * count;
            var result =
                _donationService.GetPackageWithTickets(new PagedResultRequestDto
                {
                    MaxResultCount = count,
                    SkipCount = offset
                });
            return View(result);
        }

        public ActionResult Match(int packageId)
        {
            var result = _donationService.MatchPackage(new MatchPackageInput {PackageId = packageId});
            if(result.Success) FlashSuccess(result.Message);
            else FlashError(result.Message);
            return RedirectToAction("Confirmed");
        }

        public ActionResult Pair(string phone, int packageId)
        {
            var user = _userRepository.FirstOrDefault(u => u.PhoneNumber == phone);
            var beneficiaryPackage =
                _packageRepository.FirstOrDefault(p => p.UserId == user.Id && p.Status == Status.Confirm);
            if (beneficiaryPackage == null)
            {
                FlashError("The selected user have no confirmed package");
                return RedirectToAction("Index", "Packages");
            }

            var package = _packageRepository.Get(packageId);
            if (package.Status != Status.Pending)
            {
                FlashError("This package have already been paired");
                return RedirectToAction("Index", new { id = Status.Pending });
            }
            var type = _packageTypeRepository.Get(package.TypeId);
            var donation = new Donation
            {
                Amount = type.Price,
                Status = Status.Pending,
                Date = Clock.Now,
                FromUserId = package.UserId,
                ToUserId = user.Id,
            };
            _donationRepository.Insert(donation);
            package.Status = Status.Paired;
            _packageRepository.Update(package);
            //if this is the last pairing for the beneficiary
            FlashSuccess("Donation created successfully");
            return RedirectToAction("Index");
        }
    }
}