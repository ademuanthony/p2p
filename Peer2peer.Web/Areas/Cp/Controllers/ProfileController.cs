using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Peer2peer.Users;
using Peer2peer.Web.Controllers;

namespace Peer2peer.Web.Areas.Cp.Controllers
{
    public class ProfileController : BackendControllerBase
    {
        public ProfileController(IUserAppService userService) : base(userService)
        {
            
        }

        // GET: Cp/Profile
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUser();
            return View(user);
        }

        [HttpPost]
        public ActionResult Update(User user)
        {
            return RedirectToAction("Index");
        }
    }
}