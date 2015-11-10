﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;
using TMNT.Helpers;
using TMNT.Filters;
using TMNT.Models.Enums;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class BalanceController : Controller {

        private IRepository<Device> repo;
        public BalanceController()
            : this(new DeviceRepository(DbContextSingleton.Instance)) {
        }

        public BalanceController(IRepository<Device> repo) {
            this.repo = repo;
        }

        // GET: /ScaleTest/
        [Route("Balances")]
        public ActionResult Index() {
            var department = HelperMethods.GetUserDepartment();

            var balances = repo.Get().Where(item => item.DeviceType.Equals("Balance") && item.Department.Equals(department) && !item.IsArchived);
            var viewModels = new List<BalanceIndexViewModel>();

            foreach (var item in balances) {
                CompareDates.SetBalanceToUnverified(item);
                viewModels.Add(new BalanceIndexViewModel() { 
                    BalanceId = item.DeviceId,
                    DeviceCode = item.DeviceCode,
                    IsVerified = item.IsVerified,
                    DepartmentName = item.Department.DepartmentName,
                    LastVerifiedBy = item.DeviceVerifications == null ? "N/A" :
                                item.DeviceVerifications
                                .Where(x => x.Device.Equals(item))
                                .Count() == 0 ?
                                    "N/A" :
                                    item.DeviceVerifications
                                        .Where(x => x.Device.Equals(item))
                                        .OrderBy(x => x.VerifiedOn)
                                        .Select(x => x.User.UserName)
                                        .First()
                });
            }
            return View(viewModels);
        }

        // GET: /ScaleTest/Details/5
        [Route("Balance/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Device device = repo.Get(id);

            if (device == null) {
                return HttpNotFound();
            }

            BalanceVerificationViewModel balanceData = new BalanceVerificationViewModel() {
                BalanceId = device.DeviceId,
                Department = device.Department,
                DeviceCode = device.DeviceCode,
                IsVerified = device.IsVerified,
                Location = device.Department.Location,
                Status = device.Status
            };

            if (device.DeviceVerifications.Count > 0) {
                balanceData.DeviceVerifications = device.DeviceVerifications.OrderByDescending(x => x.VerifiedOn).ToList();
                balanceData.LastVerified = device.DeviceVerifications.OrderByDescending(item => item.VerifiedOn).Select(item => item.VerifiedOn).First();
                balanceData.LastVerifiedBy = device.DeviceVerifications.OrderByDescending(item => item.VerifiedOn).Select(item => item.User.UserName).First();
            }
            return View(balanceData);
        }

        [Route("Balance/Create")]
        public ActionResult Create() {
            return View(SetCreateBalance(new BalanceCreateViewModel()));
        }

        [Route("Balance/Create")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "BalanceId,DeviceCode,LocationName,DepartmentName,SubDepartmentName,NumberOfDecimals,WeightLimitOne,WeightLimitTwo,WeightLimitThree,NumberOfTestsToVerify")] BalanceCreateViewModel balance, string submit) {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid) {
                var deviceRepo = new DeviceRepository();

                var doesDeviceAlreadyExist = deviceRepo.Get().Any(item => item != null && item.DeviceCode.Equals(balance.DeviceCode));

                if (doesDeviceAlreadyExist) {
                    ModelState.AddModelError("", "The Device Code provided is not unique. Please try again.");
                    return View(SetCreateBalance(balance));
                }

                var device = new Device() {
                    AmountLimitOne = balance.WeightLimitOne.ToString(),
                    AmountLimitTwo = balance.WeightLimitTwo.ToString(),
                    AmountLimitThree = balance.WeightLimitThree.ToString(),
                    DeviceCode = balance.DeviceCode,
                    NumberOfDecimals = balance.NumberOfDecimals,
                    Status = "Needs Verification",
                    IsVerified = false,
                    DeviceType = "Balance",
                    NumberOfTestsToVerify = balance.NumberOfTestsToVerify,
                    Department = new DepartmentRepository().Get().Where(item => item.DepartmentName.Equals(balance.DepartmentName) && item.SubDepartment.Equals(balance.SubDepartmentName)).First()
                };

                var result = deviceRepo.Create(device);

                switch (result) {
                    case CheckModelState.Invalid:
                        ModelState.AddModelError("", "The creation of the balance failed. Please double check all inputs and try again.");
                        return View(SetCreateBalance(balance));
                    case CheckModelState.DataError:
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                        return View(SetCreateBalance(balance));
                    case CheckModelState.Error:
                        ModelState.AddModelError("", "There was an error. Please try again.");
                        return View(SetCreateBalance(balance));
                    case CheckModelState.Valid:
                        if (!string.IsNullOrEmpty(submit) && submit.Equals("Save")) {
                            //save pressed
                            return RedirectToAction("Index");
                        } else {
                            //save & new pressed
                            return RedirectToAction("Create");
                        }
                    default:
                        ModelState.AddModelError("", "An unknown error occurred.");
                        return View(SetCreateBalance(balance));
                }
            }
            return View(SetCreateBalance(new BalanceCreateViewModel()));
        }

        // GET: /ScaleTest/Create
        [Route("Balance/Verification")]
        public ActionResult VerificationUnspecified() {
            //sending all Locations to the view
            ViewBag.Locations = new LocationRepository(DbContextSingleton.Instance).Get().Select(name => name.LocationName).ToList();
            return View("Verification");
        }

        [Route("Balance/Verification/{id?}")]
        public ActionResult Verification(int? id) {
            //sending all Locations to the view
            ViewBag.Locations = new LocationRepository(DbContextSingleton.Instance).Get().Select(name => name.LocationName).ToList();
            var balance = repo.Get(id);

            ViewBag.DeviceCode = balance.DeviceCode;
            ViewBag.SelectedLocation = balance.Department.Location.LocationName;

            var device = new BalanceVerificationViewModel() {
                BalanceId = balance.DeviceId,
                Location = balance.Department.Location,
                DeviceCode = balance.DeviceCode,
                NumberOfTestsToVerify = balance.NumberOfTestsToVerify,
                WeightLimitOne = balance.AmountLimitOne,
                WeightLimitTwo = balance.AmountLimitTwo,
                WeightLimitThree = balance.AmountLimitThree
            };

            return View(device);
        }

        // POST: /ScaleTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Balance/Verification")]
        public ActionResult CreateVerification([Bind(Include = "BalanceId,WeightId,DeviceCode,WeightOne,WeightTwo,WeightThree,Comments,WeightId")] BalanceVerificationViewModel balancetest,
            string[] WeightOneTable, string[] WeightTwoTable, string[] WeightThreeTable, string[] PassOrFailTable) {

            if (!ModelState.IsValid) {
                var errors = ModelState.Where(item => item.Value.Errors.Any());
                return View(SetVerificationBalance(balancetest));
            }

            if (WeightOneTable == null || WeightTwoTable == null || WeightThreeTable == null || PassOrFailTable == null) {
                ModelState.AddModelError("", "Writing the device verification failed. Make sure the device verification is complete and try again.");
                return View(SetVerificationBalance(balancetest));
            }
            //if all 3 arrays are not of equal length, return to view with an error message
            if (!(WeightOneTable.Length == WeightTwoTable.Length) || !(WeightThreeTable.Length == PassOrFailTable.Length)) {
                ModelState.AddModelError("", "Writing the device verification failed. Make sure the device verification is complete and try again.");
                return View(SetVerificationBalance(balancetest));
            }
            
            balancetest.BalanceId = repo.Get().Where(item => item.DeviceCode.Equals(balancetest.DeviceCode)).Select(item => item.DeviceId).First();

            var balance = repo.Get(balancetest.BalanceId);
            var result = CheckModelState.Invalid;
            var user = HelperMethods.GetCurrentUser();
            var verification = new DeviceVerification();

            foreach (var item in PassOrFailTable) {
                verification = new DeviceVerification() {
                    DidTestPass = item.Equals("Pass") ? true : false,
                    VerifiedOn = DateTime.Today,
                    WeightOne = balancetest.WeightOne,
                    WeightTwo = balancetest.WeightTwo,
                    WeightThree = balancetest.WeightThree,
                    WeightId = balancetest.WeightId,
                    Device = repo.Get(balancetest.BalanceId),
                    User = user
                };

                result = new DeviceVerificationRepostory().Create(verification);

                if (result != CheckModelState.Valid) {
                    //writing to db didn't work, break from loop
                    break;
                }
                //add verification to the balance
                balance.DeviceVerifications.Add(verification);
            }

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The verification of " + balancetest.DeviceCode + " failed. Please double check all inputs and try again.");
                    return View(SetVerificationBalance(balancetest));
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    return View(SetVerificationBalance(balancetest));
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    return View(SetVerificationBalance(balancetest));
                case CheckModelState.Valid:
                    balance.IsVerified = verification.DidTestPass;
                    repo.Update(balance);
                    //save pressed
                    return RedirectToAction("Index");
                default:
                    ModelState.AddModelError("", "An unknown error occurred.");
                    return View(SetVerificationBalance(balancetest));
            }
        }

        // GET: /ScaleTest/Edit/5
        [Route("Balance/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = repo.Get(id);
            if (device == null) {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: /ScaleTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Balance/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceTestId")] Device device) {
            if (ModelState.IsValid) {
                repo.Update(device);
                return RedirectToAction("Index");
            }
            return View(device);
        }

        // GET: /ScaleTest/Delete/5
        [Route("Balance/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = repo.Get(id);
            if (device == null) {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: /ScaleTest/Delete/5
        [Route("Balance/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id, string submit) {
            //don't want to delete, return to index
            if (submit.Contains("No")) {
                return RedirectToAction("Index");
            }

            var device = repo.Get(id);
            var result = repo.Delete(id);

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The verification of " + device.DeviceCode + " failed. Please double check all inputs and try again.");
                    return View(device);
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    return View(device);
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    return View(device);
                case CheckModelState.Valid:
                    //save pressed
                    return RedirectToAction("Index");
                default:
                    ModelState.AddModelError("", "An unknown error occurred.");
                    return View(device);
            }
        }

        private BalanceCreateViewModel SetCreateBalance(BalanceCreateViewModel model) {
            var locations = new LocationRepository(DbContextSingleton.Instance).Get();
            var departments = new DepartmentRepository(DbContextSingleton.Instance).Get();

            model.LocationNames = locations.Select(item => item.LocationName).ToList();
            model.Departments = departments
                .Where(item => !item.DepartmentName.Equals("Quality Assurance"))
                .GroupBy(item => item.DepartmentName)
                .Select(item => item.First()).ToList();
            model.SubDepartments = departments
                .Where(item => !string.IsNullOrEmpty(item.SubDepartment) || !item.DepartmentName.Equals("Quality Assurance"))
                .ToList();
            model.WeightUnits = new UnitRepository().Get().Where(item => item.UnitType.Equals("Weight")).Select(item => item.UnitShorthandName).ToList();

            return model;
        }

        private BalanceVerificationViewModel SetVerificationBalance(BalanceVerificationViewModel model) {

            //TODO use in post method of verification

            return model;
        }
    }
}
