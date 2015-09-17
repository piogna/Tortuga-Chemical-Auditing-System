using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    [Authorize]
    public class MaxxamMadeStandardController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IRepository<MaxxamMadeStandard> repoStandard;
        public MaxxamMadeStandardController()
            : this(new MaxxamMadeStandardRepository(DbContextSingleton.Instance)) {
        }

        public MaxxamMadeStandardController(IRepository<MaxxamMadeStandard> repoStandard) {
            this.repoStandard = repoStandard;
        }

        // GET: MaxxamMadeStandards
        [Route("InHouseStandard")]
        public ActionResult Index() {
            var standards = repoStandard.Get();
            List<MaxxamMadeStandardViewModel> list = new List<MaxxamMadeStandardViewModel>();

            foreach (var item in standards) {
                list.Add(new MaxxamMadeStandardViewModel() {
                    MaxxamMadeStandardId = item.MaxxamMadeStandardId,
                    MaxxamId = item.MaxxamId,
                    MaxxamMadeStandardName = item.MaxxamMadeStandardName,
                    IdCode = item.IdCode,
                    LastModifiedBy = item.LastModifiedBy,
                    Purity = item.Purity,
                    SolventUsed = item.SolventUsed
                });
            }
            //iterating through the associated InventoryItem and retrieving the appropriate data
            //this is faster than LINQ
            int counter = 0;
            foreach (var standard in standards) {
                foreach (var invItem in standard.InventoryItems) {
                    if (standard.MaxxamMadeStandardId == invItem.MaxxamMadeStandard.MaxxamMadeStandardId) {
                        list[counter].CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].UsedFor = invItem.UsedFor;
                        list[counter].Unit = invItem.Unit;
                        list[counter].CatalogueCode = invItem.CatalogueCode;
                        list[counter].ExpiryDate = invItem.ExpiryDate;
                        list[counter].IsExpired = invItem.ExpiryDate.Date >= DateTime.Today;
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
