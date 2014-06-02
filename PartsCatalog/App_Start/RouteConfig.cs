using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PartsCatalog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{makeId}/{modelId}",
                defaults: new { controller = "Makes", action = "List", makeId = UrlParameter.Optional, modelId = UrlParameter.Optional }
            );

        }
    }
}