using System.Web.Mvc;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using Peer2peer.Sessions;
using Peer2peer.Web.Models.Layout;

namespace Peer2peer.Web.Controllers
{
    public class LayoutController : Peer2peerControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ILanguageManager _languageManager;

        public LayoutController(
            IUserNavigationManager userNavigationManager, 
            ISessionAppService sessionAppService, 
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager)
        {
            _userNavigationManager = userNavigationManager;
            _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
        }

        [ChildActionOnly]
        public PartialViewResult TopMenu(string activeMenu = "")
        {
            var model = new TopMenuViewModel
                        {
                            MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.ToUserIdentifier())),
                            ActiveMenuItemName = activeMenu
                        };

            return PartialView("_TopMenu", model);
        }

        [ChildActionOnly]
        public PartialViewResult LanguageSelection()
        {
            var model = new LanguageSelectionViewModel
                        {
                            CurrentLanguage = _languageManager.CurrentLanguage,
                            Languages = _languageManager.GetLanguages()
                        };

            return PartialView("_LanguageSelection", model);
        }

        [ChildActionOnly]
        public PartialViewResult UserMenuOrLoginLink()
        {
            UserMenuOrLoginLinkViewModel model;

            if (AbpSession.UserId.HasValue)
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations()),
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                };
            }
            else
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled                    
                };
            }

            return PartialView("_UserMenuOrLoginLink", model);
        }

        [ChildActionOnly]
        public PartialViewResult MonachTopBar()
        {
            UserMenuOrLoginLinkViewModel model;

            if (AbpSession.UserId.HasValue)
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    LoginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations()),
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                };
            }
            else
            {
                model = new UserMenuOrLoginLinkViewModel
                {
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled
                };
            }

            return PartialView("__MonachTopBar", model);
        }

        [ChildActionOnly]
        public PartialViewResult MonachSideBar(string activeMenu = "")
        {
            var model = new TopMenuViewModel
            {
                MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("SideMenu", AbpSession.ToUserIdentifier())),
                ActiveMenuItemName = activeMenu
            };

            return PartialView( model);
        }
    }
}