using System.Web.Mvc;

namespace Peer2peer.Web.Areas.Backbone
{
    public class BackboneAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Backbone";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Backbone_default",
                "Backbone/{controller}/{action}/{id}",
                new { action = "Index", controller = "Pakcages", id = UrlParameter.Optional }
            );
        }
    }
}