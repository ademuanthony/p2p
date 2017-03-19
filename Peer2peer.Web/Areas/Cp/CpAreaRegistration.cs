using System.Web.Mvc;

namespace Peer2peer.Web.Areas.Cp
{
    public class CpAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cp_default",
                "Cp/{controller}/{action}/{id}",
                new { action = "Index", controller = "dashboard", id = UrlParameter.Optional }
            );
        }
    }
}