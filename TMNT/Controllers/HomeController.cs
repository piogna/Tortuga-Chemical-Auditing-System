using System;
using System.Linq;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    [Authorize]
    public class HomeController : Controller {

        [Route("")]
        public ActionResult Index() {
            var model = new DashboardViewModel();
            var inventoryRepo = new InventoryItemRepository(DbContextSingleton.Instance).Get();

            var reagents = new StockReagentRepository(DbContextSingleton.Instance).Get().ToList();
            var standards = new StockStandardRepository(DbContextSingleton.Instance).Get().ToList();

            Department userDepartment = Helpers.HelperMethods.GetUserDepartment();

            var expiringItems = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today.AddDays(30) && !(item.ExpiryDate < DateTime.Today) && item.Department == userDepartment).Count();
            var expiredItems = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today && item.Department == userDepartment);
            var cofas = new CertificateOfAnalysisRepository(DbContextSingleton.Instance).Get().Count();

            model.ExpiringItemsCount = expiringItems;
            model.ExpiredItems = expiredItems;
            model.CertificatesCount = cofas;

            model.PendingVerificationCount = new DeviceRepository(DbContextSingleton.Instance).Get().Where(item => !item.IsVerified && item.Department == userDepartment).Count();

            model.Department = userDepartment.DepartmentCode;
            model.LocationName = userDepartment.Location.LocationName;

            return View(model);
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