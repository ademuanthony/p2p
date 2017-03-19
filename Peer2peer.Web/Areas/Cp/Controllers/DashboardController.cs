using System.Data.Entity;
using System.Linq;
using Abp.Domain.Repositories;
using Peer2peer.EntityFramework.Repositories;
using Peer2peer.Web.Areas.Cp.Models;
using Peer2peer.Web.Controllers;
using System.Web.Mvc;
using Peer2peer.Users;

namespace Peer2peer.Web.Areas.Cp.Controllers
{
    public class DashboardController : BackendControllerBase
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IRepository<Package> _packageRepository;

        public DashboardController(IUserAppService userService, 
            IDonationRepository donationRepository, IRepository<Package> packageRepository):base(userService)
        {
            _donationRepository = donationRepository;
            _packageRepository = packageRepository;
        }

        [Authorize]
        // GET: Dashboard
        public ActionResult Index()
        {
            
            var viewModel = new DashboardViewModel
            {
                Transactions = _donationRepository.GetTransactions(AbpSession.UserId.Value),
                PendingDonation =
                    _donationRepository.GetAll()
                        .Include(d => d.ToUser)
                        .FirstOrDefault(d => d.FromUserId == AbpSession.UserId.Value &&
                                             d.Status == Status.Pending),
                PendingPackage = _packageRepository.FirstOrDefault(p => p.UserId == AbpSession.UserId.Value &&
                                                                        p.Status != Status.PaidOut),
                PendingConfirmations = _donationRepository.GetPendingConfirmations(AbpSession.UserId.Value)
            };

            if (viewModel.PendingPackage != null && viewModel.PendingPackage.Status == Status.Pending)
            {
                FlashSuccess("You have a pendnig package. Please hold on for the system to pair you");
            }

            return View(viewModel);
        }
    }
}