using Peer2peer.Sessions.Dto;

namespace Peer2peer.Web.Models.Layout
{
    public class UserMenuOrLoginLinkViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }
        public bool IsMultiTenancyEnabled { get; set; }

        public string GetShownLoginName()
        {
            return LoginInformations.User.UserName;
        }

        public UserLoginInfoDto User => LoginInformations.User;
    }
}