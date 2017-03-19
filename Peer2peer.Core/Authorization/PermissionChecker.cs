using Abp.Authorization;
using Peer2peer.Authorization.Roles;
using Peer2peer.MultiTenancy;
using Peer2peer.Users;

namespace Peer2peer.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
