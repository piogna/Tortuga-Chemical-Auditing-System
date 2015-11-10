﻿using System;
using System.Linq;
using System.Web.Mvc;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;
using TMNT.Helpers;
using TMNT.Filters;
using System.Collections.Generic;
using TMNT.Models;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class HomeController : Controller {

        [Route("")]
        public ActionResult Index() {
            var userDepartment = HelperMethods.GetUserDepartment();
            var user = HelperMethods.GetCurrentUser();
            IEnumerable<InventoryItem> inventoryRepo = null;
            IEnumerable<Device> deviceRepo = null;

            //quality assurance can see everything
            if (userDepartment.DepartmentName.Equals("Quality Assurance")) {
                inventoryRepo = new InventoryItemRepository(DbContextSingleton.Instance).Get();
                deviceRepo = new DeviceRepository(DbContextSingleton.Instance).Get().Where(item => !item.IsVerified && !item.IsArchived);
            } else {
                inventoryRepo = new InventoryItemRepository(DbContextSingleton.Instance).Get()
                    .Where(item => item.Department == userDepartment);
                deviceRepo = new DeviceRepository(DbContextSingleton.Instance).Get().Where(item => !item.IsVerified && item.Department == userDepartment && !item.IsArchived);
            }

            var cofas = new CertificateOfAnalysisRepository().Get().Where(item => item.InventoryItem.Department == userDepartment).Count();//inventoryRepo.Select(item => item.CertificatesOfAnalysis).Count();

            if (userDepartment == null) {
                ModelState.AddModelError("", "User is not designated a department");
            }

            var model = new DashboardViewModel() {
                ExpiringItemsCount = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today.AddDays(30) && !(item.ExpiryDate < DateTime.Today)).Count(),
                ExpiredItems = inventoryRepo.Where(item => item.ExpiryDate < DateTime.Today),
                CertificatesCount = cofas,
                PendingVerificationCount = deviceRepo.Count(),
                DepartmentName = userDepartment.DepartmentName,
                SubDepartment = userDepartment.SubDepartment,
                Role = HelperMethods.GetUserRoles().First(),
                LocationName = userDepartment.Location.LocationName,
                Name = user.FirstName + " " + user.LastName
            };

            switch (model.Role) {
                case "Analyst":
                    //lab technician dashboard
                    break;
                case "Department Head":
                case "Manager":
                case "Quality Assurance":
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