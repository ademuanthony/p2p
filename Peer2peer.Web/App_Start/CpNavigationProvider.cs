using Abp.Application.Navigation;
using Abp.Localization;
using Peer2peer.Authorization;

namespace Peer2peer.Web
{
    public class CpNavigationProvider : NavigationProvider
    {
        public const string MenuName = "SideMenu";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName,
                new FixedLocalizableString("Side Menu"));

            menu
                .AddItem(
                    new MenuItemDefinition(
                        "HomePage",
                        L("HomePage"),
                        url: "/",
                        icon: "glyph-icon icon-home"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Control Panel",
                        L("ControlPanel"),
                        url: "cp",
                        icon: "glyph-icon icon-linecons-tv",
                        requiresAuthentication: true
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Profile",
                        L("Profile"),
                        url: "cp/profile",
                        icon: "glyph-icon icon-user",
                        requiresAuthentication: true
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "New Packages",
                        L("NewPackages"),
                        url: "backbone/packages/pending",
                        icon: "glyph-icon icon-linecons-diamond",
                        requiredPermissionName: PermissionNames.Pages_Users
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Comfirmed Packages",
                        L("ComfirmedPackages"),
                        url: "backbone/packages/confirmed",
                        icon: "glyph-icon icon-linecons-diamond",
                        requiredPermissionName: PermissionNames.Pages_Users
                        )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, Peer2peerConsts.LocalizationSourceName);
        }
    }
}