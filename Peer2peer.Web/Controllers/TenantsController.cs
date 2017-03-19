using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using Peer2peer.Authorization;
using Peer2peer.MultiTenancy;

namespace Peer2peer.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Tenants)]
    public class TenantsController : Peer2peerControllerBase
    {
        private readonly ITenantAppService _tenantAppService;

        public TenantsController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        public ActionResult Index()
        {
            var output = _tenantAppService.GetTenants();
            return View(output);
        }
    }
}