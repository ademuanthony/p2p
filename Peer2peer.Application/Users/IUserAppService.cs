using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Peer2peer.Users.Dto;

namespace Peer2peer.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultDto<UserListDto>> GetUsers();

        Task<User> GetUser(long id);

        Task CreateUser(CreateUserInput input);
    }
}