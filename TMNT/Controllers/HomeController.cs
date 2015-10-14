using System;
using System.Linq;
using System.Web.Mvc;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;
using TMNT.Helpers;
using TMNT.Filters;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class HomeController : Controller {

        [Route("")]
        public ActionResult Index() {
            var userDepartment = HelperMethods.GetUserDepartment();
            var user = HelperMethods.GetCurrentUser();

            var inventoryRepo = new InventoryItemRepository(DbContextSingleton.Instance).Get()
                .Where(item => item.Department == userDepartment);

            var cofas = inventoryRepo.Select(item => item.CertificatesOfAnalysis).Count();

            if (userDepartment == null) {
                ModelState.AddModelError("", "User is not designated a department");
            }

            var model = new DashboardViewModel() {
                ExpiringItemsCount = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today.AddDays(30) && !(item.ExpiryDate < DateTime.Today)).Count(),
                ExpiredItems = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today),
                CertificatesCount = cofas,
                PendingVerificationCount = new DeviceRepository(DbContextSingleton.Instance).Get().Where(item => !item.IsVerified && item.Department == userDepartment).Count(),
                Department = userDepartment.DepartmentName,
                Role = HelperMethods.GetUserRoles().First(),
                LocationName = userDepartment.Location.LocationName,
                Name = user.FirstName + " " + user.LastName
            };

            switch (model.Role) {
                case "Lab Technician":
                    //lab technician dashboard
                    break;
                case "Department Head":
                    //department head dashboard
                    return View(model);
                case "Administrator":
                    //admin dashboard
                    return View("AdminIndex", model);
                default:
                    //error
                    break;
            }
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