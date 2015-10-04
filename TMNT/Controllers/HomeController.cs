﻿using System;
using System.Linq;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;
using TMNT.Helpers;

namespace TMNT.Controllers {
    [Authorize]
    public class HomeController : Controller {

        [Route("")]
        public ActionResult Index() {
            var inventoryRepo = new InventoryItemRepository(DbContextSingleton.Instance).Get();
            
            Department userDepartment = HelperMethods.GetUserDepartment();

            if (userDepartment == null) {
                ModelState.AddModelError("", "User is not designated a department");
            }
            
            var cofas = new CertificateOfAnalysisRepository(DbContextSingleton.Instance).Get().Where(item => item.InventoryItem.Department == userDepartment).Count();
            var user = HelperMethods.GetCurrentUser();
            
            var model = new DashboardViewModel() {
                ExpiringItemsCount = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today.AddDays(30) && !(item.ExpiryDate < DateTime.Today) && item.Department == userDepartment).Count(),
                ExpiredItems = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today && item.Department == userDepartment),
                CertificatesCount = cofas,
                PendingVerificationCount = new DeviceRepository(DbContextSingleton.Instance).Get().Where(item => !item.IsVerified && item.Department == userDepartment).Count(),
                Department = userDepartment.DepartmentCode,
                Role = HelperMethods.GetUserRoles().First(),
                LocationName = userDepartment.Location.LocationName,
                Name = user.FirstName + " " + user.LastName
            };

            if (model.Role.Equals("Administrator")) {
                return View("AdminIndex", model);
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