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
            var inventoryRepo = new InventoryItemRepository(DbContextSingleton.Instance).Get();

            //var reagents = new StockReagentRepository(DbContextSingleton.Instance).Get().ToList();
            //var standards = new StockStandardRepository(DbContextSingleton.Instance).Get().ToList();

            Department userDepartment = Helpers.HelperMethods.GetUserDepartment();
            
            var cofas = new CertificateOfAnalysisRepository(DbContextSingleton.Instance).Get().Count();
            
            var model = new DashboardViewModel() {
                ExpiringItemsCount = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today.AddDays(30) && !(item.ExpiryDate < DateTime.Today) && item.Department == userDepartment).Count(),
                ExpiredItems = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today && item.Department == userDepartment),
                CertificatesCount = cofas,
                PendingVerificationCount = new DeviceRepository(DbContextSingleton.Instance).Get().Where(item => !item.IsVerified && item.Department == userDepartment).Count(),
                Department = userDepartment.DepartmentCode,
                LocationName = userDepartment.Location.LocationName
            };

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