using System.Threading.Tasks;
using Abp.Application.Services;
using Peer2peer.Roles.Dto;

namespace Peer2peer.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
