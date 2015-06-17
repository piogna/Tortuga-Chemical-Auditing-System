using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMNT.Controllers {
    public class SyringeTestController : Controller {
        //
        // GET: /SyringeTest/
        public ActionResult Index() {
            return View();
        }

        [Route("create/new-syringe-test")]
        public ActionResult Create() {
            return View();
        }
    }
}