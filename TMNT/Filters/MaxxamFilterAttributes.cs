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

    //don't use at this time
    [AttributeUsage(AttributeTargets.All)]
    public class ReadWriteRoleAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (HelperMethods.GetUserRoles().Contains("Quality Assurance")) {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", "");
                redirectTargetDictionary.Add("action", "InsufficientPrivileges");
                redirectTargetDictionary.Add("controller", "Account");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeRedirect : AuthorizeAttribute {
        public string RedirectUrl = "~/AccessDenied";

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
            base.HandleUnauthorizedRequest(filterContext);

            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated) {
                filterContext.Result = new RedirectResult(RedirectUrl);
            }
        }
    }
}