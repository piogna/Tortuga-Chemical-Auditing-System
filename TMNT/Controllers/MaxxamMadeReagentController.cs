using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Filters;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class MaxxamMadeReagentController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();


        private IRepository<MaxxamMadeReagent> repo;

        public MaxxamMadeReagentController() : this(new MaxxamMadeReagentRepository(DbContextSingleton.Instance)) { }

        public MaxxamMadeReagentController(IRepository<MaxxamMadeReagent> repo) {
            this.repo = repo;
        }

        // GET: MaxxamMadeReagents
        [Route("InHouseReagent")]
        public ActionResult Index() {
            var reagents = repo.Get();

            List<MaxxamMadeReagentViewModel> list = new List<MaxxamMadeReagentViewModel>();

            foreach (var item in reagents) {
                list.Add(new MaxxamMadeReagentViewModel() {
                    MaxxamMadeReagentId = item.MaxxamMadeReagentId,
                    MaxxamId = item.MaxxamId,
                    IdCode = item.IdCode,
                    MaxxamMadeReagentName = item.MaxxamMadeReagentName,
                    LastModifiedBy = item.LastModifiedBy
                });
            }

            //iterating through the associated InventoryItem and retrieving the appropriate data
            //this is faster than LINQ
            int counter = 0;
            foreach (var reagent in reagents) {
                foreach (var invItem in reagent.InventoryItems) {
                    if (reagent.MaxxamMadeReagentId == invItem.MaxxamMadeReagent.MaxxamMadeReagentId) {
                        list[counter].CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].UsedFor = invItem.UsedFor;
                        //list[counter].Unit = invItem.Unit;
                        list[counter].CatalogueCode = invItem.CatalogueCode;
                        list[counter].Grade = invItem.Grade;
                        list[counter].ExpiryDate = invItem.ExpiryDate;
                        list[counter].IsExpired = invItem.ExpiryDate.Value.Date >= DateTime.Today;
                        list[counter].DateOpened = invItem.DateOpened;
                        list[counter].IsOpened = invItem.DateOpened != null;
                        list[counter].SupplierName = invItem.SupplierName;
                        list[counter].DateCreated = invItem.DateCreated;
                        list[counter].CreatedBy = invItem.CreatedBy;
                        list[counter].DateModified = invItem.DateModified;
                    }
                }
                counter++;
            }
            return View(list);
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
        [Route("InHouseReagent/Create")]
        public ActionResult Create() {
            return View();
        }

        // POST: MaxxamMadeReagents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("InHouseReagent/Create")]
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
