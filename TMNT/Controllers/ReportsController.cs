using System.Linq;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Filters;
using Newtonsoft.Json;
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

        [Route("Report/DeviceVerificationReport")]
        public ActionResult DeviceVerificationReport() {
            var user = Helpers.HelperMethods.GetCurrentUser();
            ViewBag.User = user.FirstName + " " + user.LastName;

            return View(new DeviceRepository().Get().ToList());
        }

        [Route("Report/DeviceReportInformation")]
        public ActionResult DeviceReportInformation() {
            var devices = new DeviceRepository().Get().ToList();

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
                .Where(item => item.ExpiryDate < DateTime.Today.AddDays(30) && !(item.ExpiryDate < DateTime.Today))
                .Select(item => new ReportExpiringStockViewModel() {
                    DaysUntilExpired = item.ExpiryDate == null 
                        ? "TBD" :
                            (item.ExpiryDate - DateTime.Today).Value.Days == 0
                                ? "Expires Today"
                                : (item.ExpiryDate - DateTime.Today).Value.Days.ToString(),
                    Type = item.Type,
                    ExpiryDate = item.ExpiryDate == null
                                ? "TBD"
                                : item.ExpiryDate.ToString().Split(' ')[0],
                    DateOpened = item.DateOpened == null ?
                                    "TBD" :
                                    item.DateOpened.ToString().Split(' ')[0],
                    SupplierName = item.SupplierName,
                    Department = item.Department.DepartmentName
                })
                .ToList();

            return Json(expiringInventory, JsonRequestBehavior.AllowGet);
        }
    }
}