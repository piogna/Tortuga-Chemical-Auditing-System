using System.Web.Mvc;
using System.Linq;
using TMNT.Utils;
using Microsoft.AspNet.Identity;

namespace TMNT.Controllers {
    [Authorize]
    public class HomeController : Controller {
        [Route("")]
        public ActionResult Index() {
            return View();
        }

        [Route("Home/About")]
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Home/Contact")]
        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}