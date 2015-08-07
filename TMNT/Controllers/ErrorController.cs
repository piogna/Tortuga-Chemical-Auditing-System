using System.Web.Mvc;

namespace TMNT.Controllers {
    public class ErrorController : Controller {
        //
        // GET: /Error/NotFound
        [Route("Error/NotFound")]
        public ActionResult NotFound() {
            Response.StatusCode = 404;
            return View();
        }
        [Route("Error")]
        public ActionResult Error() {
            Response.StatusCode = 500;
            return View();
        }
        [Route("Error/UnderConstruction")]
        public ActionResult UnderConstruction() {
            return View();
        }
    }
}