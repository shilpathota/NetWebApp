using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// The namespace contains all the helper classes which helps the functionality.
/// </summary>
namespace UseCaseInjector.Helpers
{
    /// <summary>
    /// Helper class which sets the navigation based on the selected action.
    /// </summary>
    public static class MenuHelper
    {
        /// <summary>
        /// This method takes the value clicked on the view and determines the class property of the element
        /// </summary>
        /// <param name="html">Object of HtmlHelper</param>
        /// <param name="controller">Controller on which the helper action is performed</param>
        /// <param name="action">Action method on which the helper class is worked on</param>
        /// <returns>css Class details to be added in the html properties</returns>
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            const string cssClass = "nav-link active";
            var currentAction = (string)html.ViewContext.RouteData.Values["action"];
            var currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : "nav-link";
        }
    }
}
