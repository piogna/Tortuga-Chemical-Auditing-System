using System.Web.Mvc;

namespace TMNT.Controllers {
    //[Authorize]
    public class HomeController : Controller {
        [Route("")]
        public ActionResult Index() {
            return View();
        }

        [Route("Home/About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Home/Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}