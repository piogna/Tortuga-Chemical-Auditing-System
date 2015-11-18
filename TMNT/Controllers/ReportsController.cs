using System.Linq;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Filters;
using System;
using TMNT.Utils;
using TMNT.Models.ViewModels;

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
            return View();
        }

        [Route("Report/ExpiringStockReport")]
        public ActionResult ExpiringStockReport() {
            var user = Helpers.HelperMethods.GetCurrentUser();
            ViewBag.User = user.FirstName + " " + user.LastName;

            return View();
        }

        [Route("Report/CalibrationReport")]
        public ActionResult CalibrationReport() {
            return View("CalibrationReport");
        }

        [Route("Report/DailyBalanceVerificationReport")]
        public ActionResult DailyBalanceVerificationReport() {
            return View();
        }

        [Route("Report/DeviceVerificationReport")]
        public ActionResult DeviceVerificationReport() {
            var user = Helpers.HelperMethods.GetCurrentUser();
            ViewBag.User = user.FirstName + " " + user.LastName;

            return View(new BalanceDeviceRepository().Get().ToList());
        }

        [Route("Report/DeviceReportInformation")]
        public ActionResult DeviceReportInformation() {
            var devices = new BalanceDeviceRepository().Get().ToList();

            var output = devices
                .Select(item => new ReportDeviceVerificationViewModel() {
                    DeviceType = item.DeviceType,
                    DeviceCode = item.DeviceCode,
                    Department = item.Department.DepartmentName,
                    IsVerified = item.IsVerified.ToString(),
                    Status = item.Status
                });
            
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [Route("Report/ExpiringInventoryReportInformation")]
        public ActionResult ExpiringInventoryReportInformation() {
            var expiringInventory = new InventoryItemRepository(DbContextSingleton.Instance).Get()
                .Where(item => item.StockReagent != null && item.StockReagent.ExpiryDate < DateTime.Today.AddDays(30) ||
                        item.StockStandard != null && item.StockStandard.ExpiryDate < DateTime.Today.AddDays(30) ||
                        item.IntermediateStandard != null && item.IntermediateStandard.ExpiryDate < DateTime.Today.AddDays(30) ||
                        item.WorkingStandard != null && item.WorkingStandard.ExpiryDate < DateTime.Today.AddDays(30))

                .Select(item => new ReportExpiringStockViewModel() {
                    //reagent
                    //DaysUntilExpired = item.StockReagent.ExpiryDate == null
                    //    ? "TBD" :
                    //        (item.StockReagent.ExpiryDate - DateTime.Today).Value.Days == 0
                    //            ? "Expires Today" :
                    //                (item.StockReagent.ExpiryDate - DateTime.Today).Value.Days < 0
                    //                    ? "Expired"
                    //                    : (item.StockReagent.ExpiryDate - DateTime.Today).Value.Days.ToString(),


                    Type = item.Type,
                    ExpiryDate = item.StockReagent.ExpiryDate == null
                                ? "TBD"
                                : item.StockReagent.ExpiryDate.ToString().Split(' ')[0],
                    IdCode = item.StockReagent != null
                                ? item.StockReagent.IdCode
                                : item.StockStandard != null
                                    ? item.StockStandard.IdCode
                                    : item.IntermediateStandard.IdCode != null
                                        ? item.IntermediateStandard.IdCode
                                        : "Error",
                    SupplierName = item.SupplierName,
                    Department = item.Department.DepartmentName
                })
                .ToList();

            var expiringInventory2 = new InventoryItemRepository(DbContextSingleton.Instance).Get()
                .Where(item => item.StockReagent != null && item.StockReagent.ExpiryDate < DateTime.Today.AddDays(30) ||
                        item.StockStandard != null && item.StockStandard.ExpiryDate < DateTime.Today.AddDays(30) ||
                        item.IntermediateStandard != null && item.IntermediateStandard.ExpiryDate < DateTime.Today.AddDays(30) ||
                        item.WorkingStandard != null && item.WorkingStandard.ExpiryDate < DateTime.Today.AddDays(30))
                        .ToList();

            //getting all days until expiry where expiry date is not null
            var reagentExpiryDates = expiringInventory2.Where(item => item.StockReagent != null && item.StockReagent.ExpiryDate != null).Select(item => (item.StockReagent.ExpiryDate - DateTime.Today).Value.Days).ToList();
            var standardExpiryDates = expiringInventory2.Where(item => item.StockStandard != null && item.StockStandard.ExpiryDate != null).Select(item => (item.StockStandard.ExpiryDate - DateTime.Today).Value.Days).ToList();
            var intStandardExpiryDates = expiringInventory2.Where(item => item.IntermediateStandard != null && item.IntermediateStandard.ExpiryDate != null).Select(item => (item.IntermediateStandard.ExpiryDate - DateTime.Today).Value.Days).ToList();
            var workingStandardExpiryDates = expiringInventory2.Where(item => item.WorkingStandard != null && item.WorkingStandard.ExpiryDate != null).Select(item => (item.WorkingStandard.ExpiryDate - DateTime.Today).Value.Days).ToList();

            int counter = 0;
            foreach (var item in expiringInventory) {
                //TODO set days until expired
                switch (item.Type) {
                    case "Reagent":
                        item.DaysUntilExpired = reagentExpiryDates[counter].ToString();
                        break;
                    case "Standard":
                        item.DaysUntilExpired = standardExpiryDates[counter].ToString();
                        break;
                    case "Intermediate Standard":
                        item.DaysUntilExpired = intStandardExpiryDates[counter].ToString();
                        break;
                    case "Working Standard":
                        item.DaysUntilExpired = workingStandardExpiryDates[counter].ToString();
                        break;
                    default:
                        //error maybe?
                        break;
                }
            }

            return Json(expiringInventory, JsonRequestBehavior.AllowGet);
        }
    }
}