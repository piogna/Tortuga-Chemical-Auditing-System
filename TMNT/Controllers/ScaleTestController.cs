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

namespace TMNT.Controllers {
    public class ScaleTestController : Controller {
        FakeLocationRepository locRepo = new FakeLocationRepository();

        private IRepository<FakeDeviceTest> repoScaleTest;
        public ScaleTestController()
            : this(new FakeDeviceTestRepository()) {
        }

        public ScaleTestController(IRepository<FakeDeviceTest> repoScaleTest) {
            this.repoScaleTest = repoScaleTest;
        }

        // GET: /ScaleTest/
        [Route("ScaleTest")]
        public ActionResult Index() {
            return View(db.DeviceTests.ToList());
            return View();
        }

        // GET: /ScaleTest/Details/5
        [Route("ScaleTest/Details/{id?}")]
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

        // GET: /ScaleTest/Create
        [Route("ScaleTest/Create")]
        public ActionResult Create() {
            //sending all Locations to the view
            var list = locRepo.Get().ToList();
            SelectList selects = new SelectList(list, "LocationId", "LocationName");
            ViewBag.Locations = selects;
            return View();
        }

        // POST: /ScaleTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ScaleTest/Create")]
        public ActionResult Create([Bind(Include = "BalanceId,WeightOne,WeightTwo,WeightThree,Comments")] BalanceTestViewModel balancetest) {
            int? selectedValue = Convert.ToInt32(Request.Form["Location"]);
            balancetest.Location = locRepo.Get(selectedValue);
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid) {
                return View("Confirmation", balancetest);
            }

            return View(balancetest);
        }

        // GET: /ScaleTest/Edit/5
        [Route("ScaleTest/Edit/{id?}")]
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
        [Route("ScaleTest/Edit/{id?}")]
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
        [Route("ScaleTest/Delete/{id?}")]
        public ActionResult Delete(int? id) {
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

        // POST: /ScaleTest/Delete/5
        [Route("ScaleTest/Delete/{id?}")]
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
