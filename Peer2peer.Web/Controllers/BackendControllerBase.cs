using System.Threading.Tasks;
using Peer2peer.Users;

namespace Peer2peer.Web.Controllers
{
    public class BackendControllerBase : Peer2peerControllerBase
    {
        protected IUserAppService UserService { get; set; }

        public BackendControllerBase(IUserAppService userService)
        {
            UserService = userService;
        }

        protected async Task<User> GetCurrentUser()
        {
            return await UserService.GetUser(AbpSession.UserId.Value);
        }

    }
}