using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;

namespace TMNT.Controllers {
    public class MaxxamMadeStandardController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MaxxamMadeStandards
        [Route("InHouseStandard")]
        public ActionResult Index() {
            return View(db.MaxxamMadeStandard.ToList());
        }

        // GET: MaxxamMadeStandards/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaxxamMadeStandard maxxamMadeStandard = db.MaxxamMadeStandard.Find(id);
            if (maxxamMadeStandard == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeStandard);
        }

        // GET: MaxxamMadeStandards/Create
        public ActionResult Create() {
            return View();
        }

        // POST: MaxxamMadeStandards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaxxamMadeStandardId,LotNumber,IdCode,MaxxamMadeStandardName,SolventUsed,SolventSupplierName,Purity,LastModifiedBy")] MaxxamMadeStandard maxxamMadeStandard) {
            if (ModelState.IsValid) {
                db.MaxxamMadeStandard.Add(maxxamMadeStandard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maxxamMadeStandard);
        }

        // GET: MaxxamMadeStandards/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaxxamMadeStandard maxxamMadeStandard = db.MaxxamMadeStandard.Find(id);
            if (maxxamMadeStandard == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeStandard);
        }

        // POST: MaxxamMadeStandards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaxxamMadeStandardId,LotNumber,IdCode,MaxxamMadeStandardName,SolventUsed,SolventSupplierName,Purity,LastModifiedBy")] MaxxamMadeStandard maxxamMadeStandard) {
            if (ModelState.IsValid) {
                db.Entry(maxxamMadeStandard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maxxamMadeStandard);
        }

        // GET: MaxxamMadeStandards/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaxxamMadeStandard maxxamMadeStandard = db.MaxxamMadeStandard.Find(id);
            if (maxxamMadeStandard == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeStandard);
        }

        // POST: MaxxamMadeStandards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            MaxxamMadeStandard maxxamMadeStandard = db.MaxxamMadeStandard.Find(id);
            db.MaxxamMadeStandard.Remove(maxxamMadeStandard);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
