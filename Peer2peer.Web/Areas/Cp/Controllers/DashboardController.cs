using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IRepository<Referral> _referralRepository;
        private readonly IRewardDonationRepository _rewardDonationRepository;

        public DashboardController(IUserAppService userService, 
            IDonationRepository donationRepository, IRepository<Package> packageRepository,
            IRepository<Referral> referralRepository,
            IRewardDonationRepository rewardDonationRepository) :base(userService)
        {
            _donationRepository = donationRepository;
            _packageRepository = packageRepository;
            _referralRepository = referralRepository;
            _rewardDonationRepository = rewardDonationRepository;
        }

        [Authorize]
        // GET: Dashboard
        public async Task<ActionResult> Index()
        {
            var currentUser = await GetCurrentUser();

            var pendingRewardDonation = _rewardDonationRepository.GetAll()
                .Include(d => d.ToUser)
                .FirstOrDefault(d => d.FromUserId == AbpSession.UserId.Value &&
                                     d.Status == Status.Pending);

            var viewModel = new DashboardViewModel
            {
                PendingRewardDonation = pendingRewardDonation,
                CurrentUser = currentUser,
                Transactions = _donationRepository.GetTransactions(AbpSession.UserId.Value),
                PendingDonation =
                    _donationRepository.GetAll()
                        .Include(d => d.ToUser)
                        .FirstOrDefault(d => d.FromUserId == AbpSession.UserId.Value &&
                                             d.Status == Status.Pending),
                PendingPackage = _packageRepository.FirstOrDefault(p => p.UserId == AbpSession.UserId.Value &&
                                                                        p.Status != Status.PaidOut),
                PendingConfirmations = _donationRepository.GetPendingConfirmations(AbpSession.UserId.Value),
                Downlines = _referralRepository.GetAll().Where(r=>r.UserId == AbpSession.UserId.Value).Select(r=>r.Downline).ToList(),
                ReferralPendingConfirmations = _rewardDonationRepository.GetPendingConfirmations(AbpSession.UserId.Value)
            };

            if (viewModel.PendingPackage != null && viewModel.PendingPackage.Status == Status.Pending)
            {
                FlashSuccess("You have a pendnig package. Please hold on for the system to pair you");
            }

            return View(viewModel);
        }
    }
}