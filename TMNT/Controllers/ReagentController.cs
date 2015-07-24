using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;

namespace TMNT.Controllers {
    public class ReagentController : Controller {
        private IRepository<StockReagent> repo;

        public ReagentController() : this(new StockReagentRepository()) { }

        public ReagentController(IRepository<StockReagent> repo) {
            this.repo = repo;
        }
        // GET: /Reagent/
        [Route("get/all-reagents")]
        public ActionResult Index() {
            return View(repo.Get());//db.StockReagents.ToList());
        }

        // GET: /Reagent/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockReagent stockreagent = repo.Get(id);//db.StockReagents.Find(id);
            if (stockreagent == null) {
                return HttpNotFound();
            }
            return View(stockreagent);
        }

        // GET: /Reagent/Create
        [Route("create/new-reagent")]
        public ActionResult Create() {
            //var units = new UnitRepository().Get().ToList();
            //SelectList list = new SelectList(units, "UnitId", "UnitName");

            var units = new List<string>() { "mg", "ul" }; //new UnitRepository().Get().ToList();//
            SelectList list = new SelectList(units);//, "UnitId", "UnitName");
            ViewBag.Units = list;
            return View();
        }

        // POST: /Reagent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create/new-reagent")]
        public ActionResult Create([Bind(Include = "CatalogueCode,IdCode,DateEntered,DateCreated,DateModified,ReagentName,CaseNumber,Amount,Grade,UsedFor,InventoryItemName")] InventoryStockReagentViewModel model, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS) {
            int? selectedValue = Convert.ToInt32(Request.Form["Unit"]);
            model.Unit = new UnitRepository().Get(selectedValue);
            model.EnteredBy = string.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name) 
                                ? "USERID"
                                : System.Web.HttpContext.Current.User.Identity.Name.Split('@')[0];

            var errors = ModelState.Where(item => item.Value.Errors.Any());

            if (ModelState.IsValid) {
                if (uploadCofA != null) {
                    var cofa = new CertificateOfAnalysis() {
                        FileName = uploadCofA.FileName,
                        ContentType = uploadCofA.ContentType,
                        DateAdded = DateTime.Today
                    };
                    using (var reader = new System.IO.BinaryReader(uploadCofA.InputStream)) {
                        cofa.Content = reader.ReadBytes(uploadCofA.ContentLength);
                    }
                    model.CertificateOfAnalysis = cofa;
                }
                if (uploadMSDS != null) {
                    var msds = new MSDS() {
                        FileName = uploadMSDS.FileName,
                        ContentType = uploadMSDS.ContentType,
                        DateAdded = DateTime.Today
                    };
                    using (var reader = new System.IO.BinaryReader(uploadCofA.InputStream)) {
                        msds.Content = reader.ReadBytes(uploadMSDS.ContentLength);
                    }
                    model.MSDS = msds;
                }

                StockReagent reagent = new StockReagent() {
                    IdCode = model.IdCode,
                    ReagentName = model.ReagentName,
                    DateEntered = DateTime.Today,
                    EnteredBy = model.EnteredBy
                };

                InventoryItem inventoryItem = new InventoryItem() {
                    CatalogueCode = model.CatalogueCode,
                    Amount = model.Amount,
                    Grade = model.Grade,
                    CaseNumber = model.CaseNumber,
                    UsedFor = model.UsedFor,
                    CreatedBy = (User.Identity.GetUserId() != null) ? User.Identity.GetUserId() : "USERID",
                    DateCreated = DateTime.Today,
                    DateModified = DateTime.Today,
                };
                inventoryItem.MSDS.Add(model.MSDS);
                inventoryItem.CertificatesOfAnalysis.Add(model.CertificateOfAnalysis);
                reagent.InventoryItems.Add(inventoryItem);

                repo.Create(reagent);
                return View("Confirmation", model);
            }
            return View(model);
        }

        // GET: /Reagent/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockReagent stockreagent = repo.Get(id);//db.StockReagents.Find(id);
            if (stockreagent == null) {
                return HttpNotFound();
            }
            return View(stockreagent);
        }

        // POST: /Reagent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReagentId,IdCode,DateEntered,ReagentName,EnteredBy")] StockReagent stockreagent) {
            if (ModelState.IsValid) {
                //db.Entry(stockreagent).State = EntityState.Modified;
                //db.SaveChanges();
                repo.Update(stockreagent);
                return RedirectToAction("Index");
            }
            return View(stockreagent);
        }

        // GET: /Reagent/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockReagent stockreagent = repo.Get(id);//db.StockReagents.Find(id);
            if (stockreagent == null) {
                return HttpNotFound();
            }
            return View(stockreagent);
        }

        // POST: /Reagent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            //StockReagent stockreagent = db.StockReagents.Find(id);
            //db.StockReagents.Remove(stockreagent);
            //db.SaveChanges();
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
