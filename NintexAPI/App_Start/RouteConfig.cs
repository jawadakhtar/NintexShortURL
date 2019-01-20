using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NintexAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");   

            routes.MapRoute(
                name: "ToLongRoute",
                url: "{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "ToShortRoute",
                url: "ToShort/{longURL}",
                defaults: new { controller = "Home", action = "ToShort", longURL = UrlParameter.Optional }
            );
        }
    }
}
