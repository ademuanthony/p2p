using System.Web.Mvc;

namespace Peer2peer.Web.Controllers
{
    public class AboutController : Peer2peerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}