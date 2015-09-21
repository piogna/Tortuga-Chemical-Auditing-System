using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMNT.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Exception
        [Route("Exception")]
        public ActionResult Index()
        {
            throw new Exception();
            return View();
        }
    }
}