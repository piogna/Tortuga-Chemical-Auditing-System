﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using TMNT.Models.Repository;

namespace TMNT.Filters {
    [AttributeUsage(AttributeTargets.All)]
    public class PasswordChangeAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (new UnitOfWork().GetCurrentUser().IsFirstTimeLogin) {
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
        private UnitOfWork _uow = new UnitOfWork();
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            if (_uow.GetUserRoles().Contains("Quality Assurance")) {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", "");
                redirectTargetDictionary.Add("action", "InsufficientPrivileges");
                redirectTargetDictionary.Add("controller", "Account");
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
        }

        protected void Dispose(bool disposing) {
            if (disposing) {
                _uow.Dispose();
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