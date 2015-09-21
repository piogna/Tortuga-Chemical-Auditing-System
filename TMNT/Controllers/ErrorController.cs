using System.Web.Mvc;

namespace TMNT.Controllers {
    public class ErrorController : Controller {
        //
        // GET: /Error/NotFound
        public ActionResult NotFound() {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Error() {
            Response.StatusCode = 500;
            return View();
        }

        public ActionResult UnderConstruction() {
            return View();
        }


        public ActionResult Forbidden()
        {
            return View();
        }

        public ActionResult CustomError()
        {
            return View();
        }
    }
}