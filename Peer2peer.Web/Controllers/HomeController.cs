using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Peer2peer.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : Peer2peerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}