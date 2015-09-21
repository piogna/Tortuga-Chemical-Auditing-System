using System;
using System.Security.Authentication;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TMNT.Controllers;

namespace TMNT {
    public class MvcApplication : HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e) {
            System.Diagnostics.Trace.WriteLine("Enter - Application_Error");

            var httpContext = ((MvcApplication)sender).Context;

            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
            var currentController = " ";
            var currentAction = " ";

            if (currentRouteData != null) {
                if (currentRouteData.Values["controller"] != null &&
                    !string.IsNullOrEmpty(currentRouteData.Values["controller"].ToString())) {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (currentRouteData.Values["action"] != null &&
                    !string.IsNullOrEmpty(currentRouteData.Values["action"].ToString())) {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            var ex = Server.GetLastError();

            if (ex != null) {
                System.Diagnostics.Trace.WriteLine(ex.Message);

                if (ex.InnerException != null) {
                    System.Diagnostics.Trace.WriteLine(ex.InnerException);
                    System.Diagnostics.Trace.WriteLine(ex.InnerException.Message);
                }
            }

            var controller = new ErrorController();
            var routeData = new RouteData();
            var action = "CustomError";
            var statusCode = 500;

            if (ex is HttpException) {
                var httpEx = ex as HttpException;
                statusCode = httpEx.GetHttpCode();

                switch (httpEx.GetHttpCode()) {
                    case 400:
                        action = "BadRequest";
                        break;

                    case 401:
                        action = "Unauthorized";
                        break;

                    case 403:
                        action = "Forbidden";
                        break;

                    case 404:
                        action = "NotFound";
                        break;

                    case 500:
                        action = "CustomError";
                        break;

                    default:
                        action = "CustomError";
                        break;
                }
            } else if (ex is AuthenticationException) {
                action = "Forbidden";
                statusCode = 403;
            }
            else
            {
                action = "CustomError";
                statusCode = 403;
            }

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.TrySkipIisCustomErrors = true;
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;

            //controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));

        }
    }
}
