using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Microsoft.AspNet.Identity;
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

        Task<IdentityResult> ChangePassword(string currentPassword, string newPassword);

        Task<IdentityResult> UpdateUser(User user);
    }
}