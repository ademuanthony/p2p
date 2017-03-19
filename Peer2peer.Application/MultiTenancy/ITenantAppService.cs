using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Peer2peer.MultiTenancy.Dto;

namespace Peer2peer.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
