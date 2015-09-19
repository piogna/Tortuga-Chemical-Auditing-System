using System;
using System.Linq;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Controllers {
    [Authorize]
    public class HomeController : Controller {

        [Route("")]
        public ActionResult Index() {
            var reagents = new StockReagentRepository().Get().ToList();
            var standards = new StockStandardRepository().Get().ToList();

            Department userDepartment = Helpers.HelperMethods.GetUserDepartment();

            var expiringItems = new InventoryItemRepository().Get().Where(item => item.ExpiryDate < DateTime.Today.AddDays(30) && !(item.ExpiryDate < DateTime.Today) && item.Department == userDepartment);
            var expiredItems = new InventoryItemRepository().Get().Where(item => item.ExpiryDate < DateTime.Today && item.Department == userDepartment);
            var cofas = new InventoryItemRepository().Get().Select(item => item.CertificatesOfAnalysis).ToList();

            ViewBag.ExpiringItems = expiringItems.Count();
            ViewBag.ExpiredItems = expiredItems;
            ViewBag.Certificates = cofas.Count;

            ViewBag.PendingVerificationCount = new DeviceRepository().Get().Where(item => !item.IsVerified && item.Department == userDepartment).Count();

            ViewBag.Department = userDepartment.DepartmentCode;
            ViewBag.LocationName = userDepartment.Location.LocationName;

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