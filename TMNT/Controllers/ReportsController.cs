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
        private UnitOfWork _uow;
        public ReportsController()
            : this(new UnitOfWork()) {
        }

        public ReportsController(UnitOfWork uow) {
            this._uow = uow;
        }

        [Route("Report/LowStockReport")]
        public ActionResult LowStockReport() {
            return View();
        }

        [Route("Report/ExpiringStockReport")]
        public ActionResult ExpiringStockReport() {
            var user = _uow.GetCurrentUser();
            ViewBag.User = user.FirstName + " " + user.LastName;

            return View();
        }

        [Route("Report/CalibrationReport")]
        public ActionResult CalibrationReport() {
            return View("CalibrationReport");
        }

        [Route("ReportDashboard")]
        public ActionResult ReportDashboard() {
            var prepItems = _uow.PrepListItemRepository.Get();
            var userDepartment = _uow.GetUserDepartment();

            var mostUsedReagent = prepItems
                .Where(item => item.StockReagent != null && item.StockReagent.InventoryItems.First().Department == userDepartment)
                .GroupBy(item => item.StockReagent)
                .OrderByDescending(item => item.Count())
                .Select(item => item.Key.ReagentName)
                .FirstOrDefault();

            var mostUsedStandard = prepItems
                .Where(item => item.StockStandard != null && item.StockStandard.InventoryItems.First().Department == userDepartment)
                .GroupBy(item => item.StockStandard)
                .OrderByDescending(item => item.Count())
                .Select(item => item.Key.StockStandardName)
                .FirstOrDefault();

            var mostUsedIntermeidateStandard = prepItems
                .Where(item => item.IntermediateStandard != null && item.IntermediateStandard.InventoryItems.First().Department == userDepartment)
                .GroupBy(item => item.IntermediateStandard)
                .OrderByDescending(item => item.Count())
                .Select(item => item.Key.IntermediateStandardName)
                .FirstOrDefault();

            //var mostUsedWorkingStandard = prepItems
            //    .Where(item => item.WorkingStandard != null)
            //    .GroupBy(item => item.WorkingStandard)
            //    .OrderByDescending(item => item.Count())
            //    .Select(item => item.Key.WorkingStandardName)
            //    .FirstOrDefault();

            var model = new ReportDashboardViewModel() {
                MostUsedReagentName = mostUsedReagent,
                MostUsedStandardName = mostUsedStandard,
                MostUsedIntermediateStandardName = mostUsedIntermeidateStandard,
                Department = userDepartment
            };

            return View(model);
        }

        [Route("Report/DailyBalanceVerificationReport")]
        public ActionResult DailyBalanceVerificationReport() {
            var user = _uow.GetCurrentUser();
            ViewBag.User = user.FirstName + " " + user.LastName;

            return View();
        }

        [Route("Report/DeviceVerificationReport")]
        public ActionResult DeviceVerificationReport() {
            var user = _uow.GetCurrentUser();
            ViewBag.User = user.FirstName + " " + user.LastName;
            var balances = _uow.BalanceDeviceRepository.Get().ToList();

            return View(balances);
        }

        [Route("Report/DeviceReportInformation")]
        public ActionResult DeviceReportInformation() {
            var devices = _uow.BalanceDeviceRepository.Get().ToList();

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

        [Route("Report/InventoryReport")]
        public ActionResult InventoryReport() {
            var user = _uow.GetCurrentUser();
            ViewBag.User = user.FirstName + " " + user.LastName;

            return View();
        }

        [Route("Report/ExpiringInventoryReportInformation")]
        public ActionResult ExpiringInventoryReportInformation() {
            var expiringInventory = _uow.InventoryItemRepository.Get()
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

            var expiringInventory2 = _uow.InventoryItemRepository.Get()
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
            _uow.Dispose();
            return Json(expiringInventory, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}