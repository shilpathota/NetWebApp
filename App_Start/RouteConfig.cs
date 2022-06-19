using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UseCaseInjector
{
    /// <summary>
    /// This configuration file routes the controller and action methods and determines the URL based on the default settings made in this configuration file.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Lists the default routes and the URL based on controller and action methods.
        /// </summary>
        /// <param name="routes">RouteCollection object with routes determined by the application</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
