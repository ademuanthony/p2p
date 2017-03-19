using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Peer2peer.MultiTenancy;

namespace Peer2peer.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}