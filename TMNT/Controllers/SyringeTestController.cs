using System.Web.Mvc;
using TMNT.Filters;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class SyringeTestController : Controller {
        //
        // GET: /SyringeTest/
        [Route("SyringeTest")]
        public ActionResult Index() {
            return View();
        }

        [Route("SyringeTest/Create")]
        public ActionResult Create() {
            return View();
        }
    }
}