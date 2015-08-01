using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.FakeModels;
using TMNT.Models.FakeRepository;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    public class BalanceController : Controller {

        private IRepository<Device> repo;
        public BalanceController()
            : this(new DeviceRepository()) {
        }

        public BalanceController(IRepository<Device> repo) {
            this.repo = repo;
        }

        // GET: /ScaleTest/
        [Route("Balances")]
        public ActionResult Index() {
            return View(repo.Get().Where(item => item.DeviceType == "Balance"));
        }

        // GET: /ScaleTest/Details/5
        [Route("Balance/Details/{id?}")]
        public ActionResult Details(int? id) {
            throw new NotImplementedException();
            //if (id == null) {
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //DeviceTest devicetest = db.DeviceTests.Find(id);
            //if (devicetest == null) {
            //    return HttpNotFound();
            //}
            //return View(devicetest);
        }

        [Route("Balance/Create")]
        public ActionResult Create() {
            return View();
        }

        // GET: /ScaleTest/Create
        [Route("Balance/Verification")]
        public ActionResult VerificationUnspecified() {
            //sending all Locations to the view
            var list = new ApplicationDbContext().Locations.Select(name => name.LocationName).ToList();//repo.Get().Select(item => item.Department.Location).ToList();
            //SelectList selects = new SelectList(list, "LocationId", "LocationName");
            ViewBag.Locations = new ApplicationDbContext().Locations.Select(name => name.LocationName).ToList();
            return View("Verification");
        }

        [Route("Balance/Verification/{id?}")]
        public ActionResult Verification(int? id) {
            //sending all Locations to the view
            ViewBag.Locations = new ApplicationDbContext().Locations.Select(name => name.LocationName).ToList();
            var balance = repo.Get(id);

            ViewBag.DeviceCode = balance.DeviceCode;
            ViewBag.SelectedLocation = balance.Department.Location.LocationName;
            
            return View(new BalanceTestViewModel() {
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
        public ActionResult CreateVerification([Bind(Include = "BalanceId,WeightOne,WeightTwo,WeightThree,Comments")] BalanceTestViewModel balancetest) {
            int? selectedValue = Convert.ToInt32(Request.Form["Location"]);
            //balancetest.Location = repo.Get(selectedValue);
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid) {
                return View("Confirmation", balancetest);
            }

            return View(balancetest);
        }

        // GET: /ScaleTest/Edit/5
        [Route("Balance/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            //if (id == null) {
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //DeviceTest devicetest = db.DeviceTests.Find(id);
            //if (devicetest == null) {
            //    return HttpNotFound();
            //}
            //return View(devicetest);
            throw new NotImplementedException();
        }

        // POST: /ScaleTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Balance/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceTestId")] DeviceTest devicetest) {
        //    if (ModelState.IsValid) {
        //        db.Entry(devicetest).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
            //    return View(devicetest);
            throw new NotImplementedException();
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
        public ActionResult DeleteConfirmed(int id) {
            //DeviceTest devicetest = db.DeviceTests.Find(id);
            //db.DeviceTests.Remove(devicetest);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing) {
            //if (disposing) {
            //    db.Dispose();
            //}
            //base.Dispose(disposing);
        }
    }
}
