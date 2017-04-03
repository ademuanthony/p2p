using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Domain.Repositories;
using Peer2peer.Users;
using Peer2peer.Web.Controllers;

namespace Peer2peer.Web.Areas.Cp.Controllers
{
    public class ProfileController : BackendControllerBase
    {
        private IUserAppService _userAppService;
        private IRepository<User, long> _userRepository;

        public ProfileController(IUserAppService userService, IRepository<User, long> userRepository ) : base(userService)
        {
            _userAppService = userService;
            _userRepository = userRepository;
        }

        // GET: Cp/Profile
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUser();
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeBankAccount(string accountName, string accountNumber, string bankName)
        {
            var userData = _userRepository.FirstOrDefault(u => u.Id == AbpSession.UserId.Value);
            userData.BankName = bankName;
            userData.AccountName = accountName;
            userData.AccountNumber = accountNumber;

            await _userRepository.UpdateAsync(userData);
            FlashSuccess("Bank deatails saved successfully");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> ChangePasswrod(string oldPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                FlashError("The New Password and Confirm Password field must be the same");
                return RedirectToAction("Index");
            }
            var result = await _userAppService.ChangePassword(oldPassword, newPassword);
            if (result.Succeeded)
            {
                FlashSuccess("Password changed successfully");
            }
            else
            {
                FlashError(string.Join("; ", result.Errors));
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            return RedirectToAction("Index");
        }
    }
}