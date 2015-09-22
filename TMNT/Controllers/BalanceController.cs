using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;
using Microsoft.AspNet.Identity;
using TMNT.Helpers;

namespace TMNT.Controllers {
    [Authorize]
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
            var balances = repo.Get().Where(item => item.DeviceType == "Balance" && item.Department == HelperMethods.GetUserDepartment());
            var viewModels = new List<BalanceViewModel>();

            foreach (var item in balances) {
                CompareDates.SetBalanceToUnverified(item);
                viewModels.Add(new BalanceViewModel() { 
                    BalanceId = item.DeviceId,
                    DeviceCode = item.DeviceCode,
                    Location = item.Department.Location,
                    Status = item.Status,
                    IsVerified = item.IsVerified,
                    Department = item.Department,
                    LastVerified = item.DeviceVerifications
                                .Where(x => x.Device == item)
                                .OrderByDescending(x => x.VerifiedOn)
                                .Select(x => x.VerifiedOn)
                                .Count() == 0 ?
                                    null :
                                    item.DeviceVerifications
                                        .Where(x => x.Device == item)
                                        .OrderBy(x => x.VerifiedOn)
                                        .Select(x => x.VerifiedOn)
                                        .First(),
                    User = item.DeviceVerifications
                                .Where(x => x.Device == item)
                                .OrderByDescending(x => x.VerifiedOn)
                                .Select(x => x.User)
                                .Count() == 0 ?
                                    null :
                                    item.DeviceVerifications
                                        .Where(x => x.Device == item)
                                        .OrderBy(x => x.VerifiedOn)
                                        .Select(x => x.User)
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

            BalanceViewModel balanceData = new BalanceViewModel() {
                BalanceId = device.DeviceId,
                Department = device.Department,
                DeviceCode = device.DeviceCode,
                IsVerified = device.IsVerified,
                Location = device.Department.Location,
                Status = device.Status,
            };

            if (device.DeviceVerifications.Count > 0) {
                balanceData.DeviceVerifications = device.DeviceVerifications.OrderByDescending(x => x.VerifiedOn).Take(5).ToList();
                balanceData.LastVerified = device.DeviceVerifications.OrderByDescending(item => item.VerifiedOn).Select(item => item.VerifiedOn).First();
                balanceData.LastVerifiedBy = device.DeviceVerifications.OrderByDescending(item => item.VerifiedOn).Select(item => item.User.FirstName + " " + item.User.LastName + " (" + item.User.UserName + ")").First();//last verified by

                    //BalanceId = device.DeviceId,
                    //Department = device.Department,
                    //DeviceCode = device.DeviceCode,
                    //IsVerified = device.IsVerified,
                    //Location = device.Department.Location,
                    //LastVerified = device.DeviceVerifications.OrderBy(item => item.VerifiedOn).Select(item => item.VerifiedOn).First(),
                    //Status = device.Status,
                    //LastVerifiedBy = device.DeviceVerifications.OrderBy(item => item.VerifiedOn).Select(item => item.User.FirstName + " " + item.User.LastName + " (" + item.User.UserName + ")").First()//last verified by
                    //User = device.DeviceVerifications.OrderBy(item => item.VerifiedOn).Select(item => item.User).First()
            }
            return View(balanceData);
        }

        [Route("Balance/Create")]
        public ActionResult Create() {
            return View();
        }

        // GET: /ScaleTest/Create
        [Route("Balance/Verification")]
        public ActionResult VerificationUnspecified() {
            var locations = new LocationRepository(DbContextSingleton.Instance);
            //sending all Locations to the view
            var list = locations.Get().Select(name => name.LocationName).ToList();//repo.Get().Select(item => item.Department.Location).ToList();
            //SelectList selects = new SelectList(list, "LocationId", "LocationName");
            ViewBag.Locations = locations.Get().Select(name => name.LocationName).ToList();
            return View("Verification");
        }

        [Route("Balance/Verification/{id?}")]
        public ActionResult Verification(int? id) {
            //sending all Locations to the view
            ViewBag.Locations = new LocationRepository(DbContextSingleton.Instance).Get().Select(name => name.LocationName).ToList();
            var balance = repo.Get(id);

            ViewBag.DeviceCode = balance.DeviceCode;
            ViewBag.SelectedLocation = balance.Department.Location.LocationName;
            
            return View(new BalanceViewModel() {
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
        public ActionResult CreateVerification([Bind(Include = "BalanceId,DeviceCode,WeightOne,WeightTwo,WeightThree,Comments")] BalanceViewModel balancetest) {
            string selectedValue = Request.Form["Type"];
            balancetest.BalanceId = repo.Get().Where(item => item.DeviceCode == balancetest.DeviceCode).Select(item => item.DeviceId).First();
            
            if (!User.Identity.IsAuthenticated || User == null) {
                return RedirectToAction("Login", "Account");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid) {
                var balance = repo.Get(balancetest.BalanceId);
                balance.IsVerified = true;

                DeviceVerification verification = new DeviceVerification() {
                    VerifiedOn = DateTime.Now,
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

        protected override void Dispose(bool disposing) {
            //if (disposing) {
            //    db.Dispose();
            //}
            //base.Dispose(disposing);
        }
    }
}
