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
    public class MaxxamMadeReagentController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MaxxamMadeReagents
        [Route("InHouseReagent")]
        public ActionResult Index() {
            return View(db.MaxxamMadeReagent.ToList());
        }

        // GET: MaxxamMadeReagents/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaxxamMadeReagent maxxamMadeReagent = db.MaxxamMadeReagent.Find(id);
            if (maxxamMadeReagent == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeReagent);
        }

        // GET: MaxxamMadeReagents/Create
        public ActionResult Create() {
            return View();
        }

        // POST: MaxxamMadeReagents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaxxamMadeReagentId,LotNumber,IdCode,MaxxamMadeReagentName,LastModifiedBy")] MaxxamMadeReagent maxxamMadeReagent) {
            if (ModelState.IsValid) {
                db.MaxxamMadeReagent.Add(maxxamMadeReagent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maxxamMadeReagent);
        }

        // GET: MaxxamMadeReagents/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaxxamMadeReagent maxxamMadeReagent = db.MaxxamMadeReagent.Find(id);
            if (maxxamMadeReagent == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeReagent);
        }

        // POST: MaxxamMadeReagents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaxxamMadeReagentId,LotNumber,IdCode,MaxxamMadeReagentName,LastModifiedBy")] MaxxamMadeReagent maxxamMadeReagent) {
            if (ModelState.IsValid) {
                db.Entry(maxxamMadeReagent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maxxamMadeReagent);
        }

        // GET: MaxxamMadeReagents/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaxxamMadeReagent maxxamMadeReagent = db.MaxxamMadeReagent.Find(id);
            if (maxxamMadeReagent == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeReagent);
        }

        // POST: MaxxamMadeReagents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            MaxxamMadeReagent maxxamMadeReagent = db.MaxxamMadeReagent.Find(id);
            db.MaxxamMadeReagent.Remove(maxxamMadeReagent);
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
