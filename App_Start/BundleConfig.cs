using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace UseCaseInjector
{
    /// <summary>
    /// The Bundle configuration bundles all the files used such as javascript, css files together and can be easily rendered in the html file.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// The method is called in cshtml using Scripts.render which bundles all the javascript and css files together so that there is no need of importing each file individually.
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                        "~/Scripts/UserDefined.js", 
                        "~/Scripts/jquery-{version}.js", 
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap_singlepage.css"
                      ));
        }
    }
}