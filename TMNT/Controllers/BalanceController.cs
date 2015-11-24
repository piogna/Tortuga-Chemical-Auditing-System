using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;
using TMNT.Filters;
using TMNT.Models.Enums;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class BalanceController : Controller {

        private UnitOfWork _uow;
        public BalanceController()
            : this(new UnitOfWork()) {
        }

        public BalanceController(UnitOfWork uow) {
            _uow = uow;
        }

        // GET: /ScaleTest/
        [Route("Balances")]
        public ActionResult Index() {
            var department = _uow.GetUserDepartment();

            var balances = _uow.BalanceDeviceRepository.Get().Where(item => item.Department.Equals(department) && !item.IsArchived);
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
                                        .OrderByDescending(x => x.VerifiedOn)
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

            Device device = _uow.BalanceDeviceRepository.Get(id);

            if (device == null) {
                return HttpNotFound();
            }

            BalanceDetailsViewModel balanceData = new BalanceDetailsViewModel() {
                BalanceId = device.DeviceId,
                Department = device.Department,
                DeviceCode = device.DeviceCode,
                IsVerified = device.IsVerified,
                Location = device.Department.Location,
                Status = device.Status,
                NumberOfDecimals = device.NumberOfDecimals
            };

            if (device.DeviceVerifications.Count > 0) {
                balanceData.DeviceVerifications = device.DeviceVerifications.OrderByDescending(x => x.VerifiedOn.Value.Date).ToList();
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

                var doesDeviceAlreadyExist = _uow.BalanceDeviceRepository.Get().Any(item => item != null && item.DeviceCode.Equals(balance.DeviceCode));

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
                    Department = _uow.DepartmentRepository.Get().Where(item => item.DepartmentName.Equals(balance.DepartmentName) && item.SubDepartment.Equals(balance.SubDepartmentName)).First()
                };

                _uow.BalanceDeviceRepository.Create(device);
                var result = _uow.Commit();

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

        [Route("Balance/Verification/{id?}")]
        public ActionResult Verification(int? id) {
            //sending all Locations to the view
            var locations = _uow.LocationRepository.Get().Select(name => name.LocationName).ToList();
            var balance = _uow.BalanceDeviceRepository.Get(id);

            var device = new BalanceVerificationViewModel() {
                BalanceId = balance.DeviceId,
                Location = balance.Department.Location,
                DeviceCode = balance.DeviceCode,
                CurrentLocation = balance.Department.Location.LocationName,
                LocationNames = locations,
                NumberOfTestsToVerify = balance.NumberOfTestsToVerify,
                WeightLimitOne = balance.AmountLimitOne + " g",
                WeightLimitTwo = balance.AmountLimitTwo + " g ",
                WeightLimitThree = balance.AmountLimitThree == null ? null : balance.AmountLimitThree + " g",
                NumberOfDecimals = balance.NumberOfDecimals
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
                return View("Verification", SetVerificationBalance(balancetest));
            }

            if (WeightOneTable == null || WeightTwoTable == null || WeightThreeTable == null || PassOrFailTable == null) {
                ModelState.AddModelError("", "Writing the device verification failed. Make sure the device verification is complete and try again.");
                return View("Verification", SetVerificationBalance(balancetest));
            }
            //if all 3 arrays are not of equal length, return to view with an error message
            if (!(WeightOneTable.Length == WeightTwoTable.Length) || !(WeightThreeTable.Length == PassOrFailTable.Length)) {
                ModelState.AddModelError("", "Writing the device verification failed. Make sure the device verification is complete and try again.");
                return View("Verification", SetVerificationBalance(balancetest));
            }

            var balance = _uow.BalanceDeviceRepository.Get().Where(item => item.DeviceCode.Equals(balancetest.DeviceCode)).First();
            var result = CheckModelState.Invalid;
            var user = _uow.GetCurrentUser();
            var verification = new DeviceVerification();

            //arrays are aligned, so let's use a traditional for-loop
            for (int i = 0; i < PassOrFailTable.Length; i++) {
                verification = new DeviceVerification() {
                    DidTestPass = PassOrFailTable[i].Equals("Pass") ? true : false,
                    VerifiedOn = DateTime.Today,
                    WeightOne = string.IsNullOrEmpty(WeightOneTable[i]) ? (double?)null : Convert.ToDouble(WeightOneTable[i]),
                    WeightTwo = string.IsNullOrEmpty(WeightTwoTable[i]) ? (double?)null : Convert.ToDouble(WeightTwoTable[i]),
                    WeightThree = string.IsNullOrEmpty(WeightThreeTable[i]) ? (double?)null : Convert.ToDouble(WeightThreeTable[i]),
                    WeightId = balancetest.WeightId,
                    Device = balance,
                    User = user
                };

                _uow.DeviceVerificationRepostory.Create(verification);
                result = _uow.Commit();

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
                    return View("Verification", SetVerificationBalance(balancetest));
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    return View("Verification", SetVerificationBalance(balancetest));
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    return View("Verification", SetVerificationBalance(balancetest));
                case CheckModelState.Valid:
                    balance.IsVerified = verification.DidTestPass;
                    balance.Status = "In Good Standing";
                    _uow.BalanceDeviceRepository.Update(balance);
                    //save pressed
                    return RedirectToAction("Index");
                default:
                    ModelState.AddModelError("", "An unknown error occurred.");
                    return View("Verification", SetVerificationBalance(balancetest));
            }
        }

        // GET: /ScaleTest/Edit/5
        [Route("Balance/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = _uow.BalanceDeviceRepository.Get(id);
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
                _uow.BalanceDeviceRepository.Update(device);
                _uow.Commit();
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
            Device device = _uow.BalanceDeviceRepository.Get(id);
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

            var device = _uow.BalanceDeviceRepository.Get(id);
            _uow.BalanceDeviceRepository.Delete(id);
            var result = _uow.Commit();

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
            var locations = _uow.LocationRepository.Get();
            var departments = _uow.DepartmentRepository.Get();

            model.LocationNames = locations.Select(item => item.LocationName).ToList();
            model.Departments = departments
                .Where(item => !item.DepartmentName.Equals("Quality Assurance"))
                .GroupBy(item => item.DepartmentName)
                .Select(item => item.First()).ToList();
            model.SubDepartments = departments
                .Where(item => !string.IsNullOrEmpty(item.SubDepartment) || !item.DepartmentName.Equals("Quality Assurance"))
                .ToList();
            model.WeightUnits = _uow.UnitRepository.Get().Where(item => item.UnitType.Equals("Weight")).Select(item => item.UnitShorthandName).ToList();

            return model;
        }

        private BalanceVerificationViewModel SetVerificationBalance(BalanceVerificationViewModel model) {
            //TODO use in post method of verification
            var locations = _uow.LocationRepository.Get();
            var departments = _uow.DepartmentRepository.Get(); ;

            model.LocationNames = locations.Select(item => item.LocationName).ToList();
            model.Department = departments.Where(item => item.DepartmentId == model.Department.DepartmentId).First();
            model.DeviceCode = model.DeviceCode;
            model.CurrentLocation = model.CurrentLocation;
            model.NumberOfDecimals = model.NumberOfDecimals;

            return model;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
