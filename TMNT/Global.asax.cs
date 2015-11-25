using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
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
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            String user = User.Identity.Name;
            if (user == "")
            {
                user = "Anonymous";
            }
            String emailBody = "<Strong>Report Time: </Strong>" + DateTime.Now + "<br/><br/>";
            emailBody += "<Strong>Browser: </Strong>" + browser.Type + ".0 <br/><br/>";
            emailBody += "<Strong>User: </Strong>" + user + "<br/><br/>";
            emailBody += "<Strong>Error Message: </Strong>" + exception.Message + "<br/><br/>";
            emailBody += "<Strong>Stack Trace: </Strong><br>" + exception.StackTrace;
            String JS = "$.ajax({ url : 'http://kal-rul.com/PHP/AJAX.php',  type: 'POST',  data : {call:'sendEmail', to:'lomasian@hotmail.ca', subject:'Tortuga UAT - " + DateTime.Now + " - Error', message:'"+ emailBody.Replace("'","\"").Replace("\r\n", "</br>") +"'}});";

            HttpContext.Current.Response.Write("<script src='https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js'></script><script>"+JS+"</script>");
            Server.ClearError();
            var httpException = exception as HttpException;

            //Logging goes here

            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Error";

            if (httpException != null)
            {
                if (httpException.GetHttpCode() == 404)
                {
                    routeData.Values["action"] = "NotFound";
                }
                Response.StatusCode = httpException.GetHttpCode();
            }
            else
            {
                Response.StatusCode = 500;
            }

            // Avoid IIS7 getting involved
            Response.TrySkipIisCustomErrors = true;

            // Execute the error controller
            IController errorsController = new ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }
    }
}
