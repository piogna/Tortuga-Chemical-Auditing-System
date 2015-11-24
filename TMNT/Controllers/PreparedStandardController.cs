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
    public class PreparedStandardController : Controller {

        private UnitOfWork _uow;
        public PreparedStandardController()
            : this(new UnitOfWork()) {

        }

        public PreparedStandardController(UnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: MaxxamMadeStandards
        [Route("PreparedStandard")]
        public ActionResult Index() {
            var standards = _uow.PreparedStandardRepository.Get();
            List<PreparedStandardViewModel> list = new List<PreparedStandardViewModel>();

            foreach (var item in standards) {
                list.Add(new PreparedStandardViewModel() {
                    PreparedStandardId = item.PreparedStandardId,
                    MaxxamId = item.MaxxamId,
                    PreparedStandardName = item.PreparedStandardName,
                    IdCode = item.IdCode,
                    LastModifiedBy = item.LastModifiedBy,
                    Purity = item.Purity,
                    SolventUsed = item.SolventUsed,
                    ExpiryDate = item.ExpiryDate,
                    DateOpened = item.DateOpened,
                    DateCreated = item.DateCreated,
                    CreatedBy = item.CreatedBy,
                    DateModified = item.DateModified
                });
            }
            //iterating through the associated InventoryItem and retrieving the appropriate data
            //this is faster than LINQ
            int counter = 0;
            foreach (var standard in standards) {
                foreach (var invItem in standard.InventoryItems) {
                    if (standard.PreparedStandardId == invItem.PreparedStandard.PreparedStandardId) {
                        list[counter].CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].UsedFor = invItem.UsedFor;
                        list[counter].CatalogueCode = invItem.CatalogueCode;
                        list[counter].IsExpired = invItem.PreparedStandard.ExpiryDate.Value.Date >= DateTime.Today;
                        list[counter].IsOpened = invItem.PreparedStandard.DateOpened != null;
                        list[counter].SupplierName = invItem.SupplierName;
                    }
                }
                counter++;
            }
            return View(list);
        }

        // GET: MaxxamMadeStandards/Details/5
        [Route("PreparedStandard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreparedStandard maxxamMadeStandard = _uow.PreparedStandardRepository.Get(id);
            if (maxxamMadeStandard == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeStandard);
        }

        // GET: MaxxamMadeStandards/Create
        [Route("PreparedStandard/Create")]
        public ActionResult Create() {
            return View();
        }

        // POST: MaxxamMadeStandards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("PreparedStandard/Create")]
        public ActionResult Create([Bind(Include = "PreparedStandardId,LotNumber,IdCode,PreparedStandardName,SolventUsed,Purity,LastModifiedBy")] PreparedStandard maxxamMadeStandard) {
            if (ModelState.IsValid) {
                _uow.PreparedStandardRepository.Create(maxxamMadeStandard);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            return View(maxxamMadeStandard);
        }

        // GET: MaxxamMadeStandards/Edit/5
        [Route("PreparedStandard/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreparedStandard maxxamMadeStandard = _uow.PreparedStandardRepository.Get(id);
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
        [Route("PreparedStandard/Edit/{id?}")]
        public ActionResult Edit([Bind(Include = "PreparedStandardId,LotNumber,IdCode,PreparedStandardName,SolventUsed,Purity,LastModifiedBy")] PreparedStandard preparedStandard) {
            if (ModelState.IsValid) {
                _uow.PreparedStandardRepository.Update(preparedStandard);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            return View(preparedStandard);
        }

        // GET: MaxxamMadeStandards/Delete/5
        [Route("PreparedStandard/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreparedStandard maxxamMadeStandard = _uow.PreparedStandardRepository.Get(id);
            if (maxxamMadeStandard == null) {
                return HttpNotFound();
            }
            return View(maxxamMadeStandard);
        }

        // POST: MaxxamMadeStandards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("PreparedStandard/Delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.PreparedStandardRepository.Delete(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
