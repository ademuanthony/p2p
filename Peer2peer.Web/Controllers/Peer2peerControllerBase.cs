using System.Collections.Generic;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using Peer2peer.Web.Models.Layout;

namespace Peer2peer.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class Peer2peerControllerBase : AbpController
    {
        protected Peer2peerControllerBase()
        {
           
            LocalizationSourceName = Peer2peerConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected void FlashSuccess(string message, string title = "")
        {
            SetFlash(new AlertViewModel {Message = message, Title = title, Type = AlertViewModel.Success});
        }

        protected void FlashError(string message, string title = "")
        {
            SetFlash(new AlertViewModel { Message = message, Title = title, Type = AlertViewModel.Error });
        }

        private void SetFlash(AlertViewModel alert)
        {
            if (TempData[AlertViewModel.Key] == null)
            {
                TempData[AlertViewModel.Key] = new List<AlertViewModel>();
            }
            ((List<AlertViewModel>)TempData[AlertViewModel.Key]).Add(alert);
        }
    }
}