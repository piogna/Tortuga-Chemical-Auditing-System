using System;
using System.Web.Mvc;
using System.Web.Routing;
using TMNT.Helpers;

namespace TMNT.Filters {
    [AttributeUsage(AttributeTargets.All)]
    public class PasswordChangeAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (HelperMethods.GetCurrentUser().IsFirstTimeLogin) {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", "");
                redirectTargetDictionary.Add("action", "ChangePasswordFirstTime");
                redirectTargetDictionary.Add("controller", "Manage");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
        }
    }
}