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

        private UnitOfWork _uow;

        public PreparedReagentController()
            : this(new UnitOfWork()) {

        }

        public PreparedReagentController(UnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: MaxxamMadeReagents
        [Route("PreparedReagent")]
        public ActionResult Index() {
            var reagents = _uow.PreparedReagentRepository.Get();

            List<PreparedReagentViewModel> list = new List<PreparedReagentViewModel>();

            foreach (var item in reagents) {
                list.Add(new PreparedReagentViewModel() {
                    PreparedReagentId = item.PreparedReagentId,
                    MaxxamId = item.MaxxamId,
                    IdCode = item.IdCode,
                    PreparedReagentName = item.PreparedReagentName,
                    LastModifiedBy = item.LastModifiedBy,
                    Grade = item.Grade,
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
            foreach (var reagent in reagents) {
                foreach (var invItem in reagent.InventoryItems) {
                    if (reagent.PreparedReagentId == invItem.PreparedReagent.PreparedReagentId) {
                        list[counter].CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].UsedFor = invItem.UsedFor;
                        list[counter].CatalogueCode = invItem.CatalogueCode;
                        list[counter].IsExpired = invItem.PreparedReagent.ExpiryDate.Value.Date >= DateTime.Today;
                        list[counter].IsOpened = invItem.PreparedReagent.DateOpened != null;
                        list[counter].SupplierName = invItem.SupplierName;
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
            PreparedReagent maxxamMadeReagent = _uow.PreparedReagentRepository.Get(id);
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
                _uow.PreparedReagentRepository.Create(preparedReagent);
                _uow.Commit();
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
            PreparedReagent maxxamMadeReagent = _uow.PreparedReagentRepository.Get(id);
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
                _uow.PreparedReagentRepository.Update(maxxamMadeReagent);
                _uow.Commit();
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
            PreparedReagent maxxamMadeReagent = _uow.PreparedReagentRepository.Get(id);
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
            _uow.PreparedReagentRepository.Delete(id);
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
