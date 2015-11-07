using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TMNT.Filters;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class PreparedReagentController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();


        private IRepository<PreparedReagent> repo;

        public PreparedReagentController() : this(new PreparedReagentRepository(DbContextSingleton.Instance)) { }

        public PreparedReagentController(IRepository<PreparedReagent> repo) {
            this.repo = repo;
        }

        // GET: MaxxamMadeReagents
        [Route("PreparedReagent")]
        public ActionResult Index() {
            var reagents = repo.Get();

            List<PreparedReagentViewModel> list = new List<PreparedReagentViewModel>();

            foreach (var item in reagents) {
                list.Add(new PreparedReagentViewModel() {
                    PreparedReagentId = item.PreparedReagentId,
                    MaxxamId = item.MaxxamId,
                    IdCode = item.IdCode,
                    PreparedReagentName = item.PreparedReagentName,
                    LastModifiedBy = item.LastModifiedBy
                });
            }

            //iterating through the associated InventoryItem and retrieving the appropriate data
            //this is faster than LINQ
            int counter = 0;
            foreach (var reagent in reagents) {
                foreach (var invItem in reagent.InventoryItems) {
                    if (reagent.PreparedReagentId == invItem.PreparedReagent.PreparedReagentId) {
                        list[counter].CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].UsedFor = invItem.UsedFor;
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
        [Route("PreparedReagent/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreparedReagent maxxamMadeReagent = db.PreparedReagent.Find(id);
            if (maxxamMadeReagent == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeReagent);
        }

        // GET: MaxxamMadeReagents/Create
        [Route("PreparedReagent/Create")]
        public ActionResult Create() {
            return View();
        }

        // POST: MaxxamMadeReagents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("PreparedReagent/Create")]
        public ActionResult Create([Bind(Include = "PreparedReagentId,LotNumber,IdCode,PreparedReagentName,LastModifiedBy")] PreparedReagent preparedReagent) {
            if (ModelState.IsValid) {
                db.PreparedReagent.Add(preparedReagent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(preparedReagent);
        }

        // GET: MaxxamMadeReagents/Edit/5
        [Route("PreparedReagent/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreparedReagent maxxamMadeReagent = db.PreparedReagent.Find(id);
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
        [Route("PreparedReagent/Edit/{id?}")]
        public ActionResult Edit([Bind(Include = "PreparedReagentId,LotNumber,IdCode,PreparedReagentName,LastModifiedBy")] PreparedReagent maxxamMadeReagent) {
            if (ModelState.IsValid) {
                db.Entry(maxxamMadeReagent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maxxamMadeReagent);
        }

        // GET: MaxxamMadeReagents/Delete/5
        [Route("PreparedReagent/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreparedReagent maxxamMadeReagent = db.PreparedReagent.Find(id);
            if (maxxamMadeReagent == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeReagent);
        }

        // POST: MaxxamMadeReagents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("PreparedReagent/Delete/{id}")]
        public ActionResult DeleteConfirmed(int id) {
            PreparedReagent maxxamMadeReagent = db.PreparedReagent.Find(id);
            db.PreparedReagent.Remove(maxxamMadeReagent);
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
