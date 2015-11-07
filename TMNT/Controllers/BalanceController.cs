using System;
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

            var balances = repo.Get().Where(item => item.DeviceType.Equals("Balance") && item.Department.Equals(department));
            var viewModels = new List<BalanceIndexViewModel>();

            foreach (var item in balances) {
                CompareDates.SetBalanceToUnverified(item);
                viewModels.Add(new BalanceIndexViewModel() { 
                    BalanceId = item.DeviceId,
                    DeviceCode = item.DeviceCode,
                    IsVerified = item.IsVerified,
                    DepartmentName = item.Department.DepartmentName,
                    LastVerifiedBy = item.DeviceVerifications//last verified by
                                .Where(x => x.Device.Equals(item))
                                .Count() == 0 ?
                                    null :
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
            return View(SetBalance(new BalanceCreateViewModel()));
        }

        [Route("Balance/Create")]
        [HttpPost]
        public ActionResult Create([Bind(Include = "BalanceId,DeviceCode,LocationName,DepartmentName,SubDepartmentName,NumberOfDecimals,WeightLimitOne,WeightLimitTwo,WeightLimitThree")] BalanceCreateViewModel balance, string submit) {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid) {
                var deviceRepo = new DeviceRepository();

                var doesDeviceAlreadyExist = deviceRepo.Get().Any(item => item != null && item.DeviceCode.Equals(balance.DeviceCode));

                if (doesDeviceAlreadyExist) {
                    ModelState.AddModelError("", "The Device Code provided is not unique. Please try again.");
                    return View(SetBalance(balance));
                }

                var device = new Device() {
                    AmountLimitOne = balance.WeightLimitOne,
                    AmountLimitTwo = balance.WeightLimitTwo,
                    AmountLimitThree = balance.WeightLimitThree,
                    DeviceCode = balance.DeviceCode,
                    NumberOfDecimals = balance.NumberOfDecimals,
                    Status = "Needs Verification",
                    IsVerified = false,
                    DeviceType = "Balance",
                    Department = new DepartmentRepository().Get().Where(item => item.DepartmentName.Equals(balance.DepartmentName) && item.SubDepartment.Equals(balance.SubDepartmentName)).First()
                };

                var result = deviceRepo.Create(device);

                switch (result) {
                    case CheckModelState.Invalid:
                        ModelState.AddModelError("", "The creation of the balance failed. Please double check all inputs and try again.");
                        return View(SetBalance(balance));
                    case CheckModelState.DataError:
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                        return View(SetBalance(balance));
                    case CheckModelState.Error:
                        ModelState.AddModelError("", "There was an error. Please try again.");
                        return View(SetBalance(balance));
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
                        return View(SetBalance(balance));
                }
            }
            return View(SetBalance(new BalanceCreateViewModel()));
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
            
            return View(new BalanceVerificationViewModel() {
                BalanceId = balance.DeviceId,
                Location = balance.Department.Location,
                DeviceCode = balance.DeviceCode
            });
        }

        // POST: /ScaleTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Balance/Verification")]
        public ActionResult CreateVerification([Bind(Include = "BalanceId,WeightId,DeviceCode,WeightOne,WeightTwo,WeightThree,Comments")] BalanceVerificationViewModel balancetest) {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid) {
                string selectedValue = Request.Form["Type"];
                balancetest.BalanceId = repo.Get().Where(item => item.DeviceCode.Equals(balancetest.DeviceCode)).Select(item => item.DeviceId).First();

                var balance = repo.Get(balancetest.BalanceId);
                balance.IsVerified = true;

                DeviceVerification verification = new DeviceVerification() {
                    DidTestPass = true,
                    VerifiedOn = DateTime.Today,
                    WeightOne = balancetest.WeightOne,
                    WeightTwo = balancetest.WeightTwo,
                    WeightThree = balancetest.WeightThree,
                    Device = repo.Get(balancetest.BalanceId),
                    User = HelperMethods.GetCurrentUser()
                };

                new DeviceVerificationRepostory().Create(verification);

                balance.DeviceVerifications.Add(verification);
                repo.Update(balance);

                return RedirectToAction("Index");
            }
            return View(balancetest);
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
        public ActionResult DeleteConfirmed(int? id) {
            repo.Delete(id);
            return RedirectToAction("Index");
        }

        private BalanceCreateViewModel SetBalance(BalanceCreateViewModel model) {
            var locations = new LocationRepository(DbContextSingleton.Instance).Get();
            var departments = new DepartmentRepository(DbContextSingleton.Instance).Get();

            model.LocationNames = locations.Select(item => item.LocationName).ToList();
            model.DepartmentNames = departments
                .Where(item => !item.DepartmentName.Equals("Quality Assurance"))
                .GroupBy(item => item.DepartmentName)
                .Select(item => item.First().DepartmentName).ToList();
            model.SubDepartmentNames = departments.Where(item => !string.IsNullOrEmpty(item.SubDepartment) || !item.DepartmentName.Equals("Quality Assurance")).Select(item => item.SubDepartment).ToList();

            return model;
        }
    }
}
