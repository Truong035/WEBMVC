using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace webMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "ThiThu",
               url: "ThiThu/{id}",
               defaults: new { controller = "Home", action = "Loald", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "LuaChon",
             url: "LuaChon/{id}",
             defaults: new { controller = "Home", action = "LuaChon", id = UrlParameter.Optional }
         );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
