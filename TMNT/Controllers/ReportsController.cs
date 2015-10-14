using System.Linq;
using System.Web.Mvc;
using TMNT.Models;
using Microsoft.AspNet.Identity;
using TMNT.Models.Repository;
using TMNT.Filters;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class ReportsController : Controller {
        private IRepository<InventoryItem> repo;
        public ReportsController()
            : this(new InventoryItemRepository()) {
        }

        public ReportsController(IRepository<InventoryItem> repo) {
            this.repo = repo;
        }

        [Route("Report/LowStockReport")]
        public ActionResult LowStockReport() {
            //List<InventoryItem> items = new InventoryItemRepository()
            //    .Get()
            //    .Where(item => item.StockReagent != null || item.StockStandard != null)
            //    .OrderBy(item => item.Amount)
            //    .ToList();

            //return View("LowStockReport", items);
            return View();
        }

        [Route("Report/CalibrationReport")]
        public ActionResult CalibrationReport() {
            return View("CalibrationReport");
        }

        [Route("Report/DeviceVerificationReport")]
        public ActionResult DeviceVerificationReport() {
            string currentUserId = User.Identity.GetUserId();
            var user = new ApplicationDbContext().Users.FirstOrDefault(x => x.Id == currentUserId);
            ViewBag.User = user.FirstName + " " + user.LastName;
            return View(new DeviceRepository().Get().ToList());
        }
    }
}