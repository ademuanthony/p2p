using System.Linq;
using System.Web.Mvc;
using Abp.Domain.Repositories;
using Abp.Timing;
using Peer2peer.Peering;
using Peer2peer.Peering.Dto;
using Peer2peer.Users;
using Peer2peer.Web.Controllers;

namespace Peer2peer.Web.Areas.Cp.Controllers
{
    public class PackagesController : BackendControllerBase
    {
        private readonly IRepository<PackageType> _packageTypeRepository;
        private readonly IRepository<Package> _packageRepository;
        private readonly IDonationAppService _donationService;

        public PackagesController(IUserAppService userAppService,
            IRepository<Package> packageRepository, IRepository<PackageType> packageTypeRepository,
            IDonationAppService donationAppService):base(userAppService)
        {
            _packageTypeRepository = packageTypeRepository;
            _packageRepository = packageRepository;
            _donationService = donationAppService;
        }

        // GET: Cp/Packages
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard", new { area = "Cp" });
        }

        public ActionResult Create(string id)
        {
            if (_packageRepository.GetAll().Any(p => p.UserId == AbpSession.UserId.Value && p.Status == Status.Pending))
            {
                FlashError("Please waiting for your current circle to be completed");
                return RedirectToAction("Index", "Dashboard");
            }
            var name = id;
            var packageType = _packageTypeRepository.FirstOrDefault(p => p.Name == name.ToLower());

            if (packageType == null)
            {
                FlashError("Invalid package selected");
                return RedirectToAction("Index", "Dashboard");
            }

            var package = new Package
            {
                UserId = AbpSession.UserId.Value,
                ExpectedRi = packageType.ReturnOnInvestment,
                CurrentRi = 0,
                TypeId = packageType.Id,
                Status = Status.Pending,
                CreatedDate = Clock.Now
            };

            _packageRepository.Insert(package);
            FlashSuccess("Your package have been added. Please wait to be paired");
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Confirm(int donationId)
        {
            var result = _donationService.ConfirmDonation(new ConfirmDonationInput {DonationId = donationId,
                CurrentUserId = AbpSession.UserId.Value
            });
            if(result.Success) FlashSuccess(result.Message);
            else FlashError(result.Message);
            return RedirectToAction("Index", "Dashboard", new {area = "Cp"});
        }

        public ActionResult ChangeStatus(int donationId, string status = Status.AwaitingAlert)
        {
            var result =
                _donationService.ChangeDonationStatus(new ChangeDonationStatusInput
                {
                    DonationId = donationId,
                    Status = status,
                    CurrentUserId = AbpSession.UserId.Value
                });
            if (result.Success) FlashSuccess(result.Message);
            else FlashError(result.Message);
            return RedirectToAction("Index", "Dashboard", new {area = "Cp"});
        }
    }
}