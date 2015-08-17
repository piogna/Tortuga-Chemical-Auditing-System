using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using System.Collections.Generic;

namespace TMNT.Controllers {
    public class ReagentController : Controller {
        private IRepository<StockReagent> repo;

        public ReagentController() : this(new StockReagentRepository()) { }

        public ReagentController(IRepository<StockReagent> repo) {
            this.repo = repo;
        }
        // GET: /Reagent/
        [Route("Reagent")]
        public ActionResult Index() {
            var reagents = repo.Get();
            List<StockReagentViewModel> list = new List<StockReagentViewModel>();

            foreach (var item in reagents) {
                list.Add(new StockReagentViewModel() {
                    ReagentId = item.ReagentId,
                    IdCode = item.IdCode,
                    DateEntered = item.DateEntered,
                    EnteredBy = item.EnteredBy,
                    ReagentName = item.ReagentName,
                    LowAmountThreshHold = item.LowAmountThreshHold,
                    LastModified = item.LastModified,
                    LastModifiedBy = item.LastModifiedBy
                });
            }

            //iterating through the associated InventoryItem and retrieving the appropriate data
            //this is faster than LINQ
            int counter = 0;
            foreach (var reagent in reagents) {
                foreach (var invItem in reagent.InventoryItems) {
                    if (reagent.ReagentId == invItem.StockReagent.ReagentId) {
                        list[counter].Amount = invItem.Amount;
                        list[counter].CaseNumber = invItem.CaseNumber;
                        list[counter].CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].UsedFor = invItem.UsedFor;
                        list[counter].Unit = invItem.Unit;
                        list[counter].CatalogueCode = invItem.CatalogueCode;
                        list[counter].Grade = invItem.Grade;
                        //list[counter].PrepListItems = new PrepListItemRepository().Get().Where(x => x.StockReagent.ReagentId == reagent.ReagentId).ToList();
                    }
                }
                counter++;
            }
            return View(list);
        }

        // GET: /Reagent/Details/5
        [Route("Reagent/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (Request.UrlReferrer == null) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            } else if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Request.UrlReferrer.AbsolutePath.Contains("IntermediateStandard")) {
                ViewBag.ReturnUrl = Request.UrlReferrer.AbsolutePath;
            }

            StockReagent reagent = repo.Get(id);

            if (reagent == null) {
                return HttpNotFound();
            }

            var vReagent = new StockReagentViewModel() {
                ReagentId = reagent.ReagentId,
                IdCode = reagent.IdCode,
                DateEntered = reagent.DateEntered,
                EnteredBy = reagent.EnteredBy,
                ReagentName = reagent.ReagentName,
                LowAmountThreshHold = reagent.LowAmountThreshHold,
                LastModified = reagent.LastModified,
                LastModifiedBy = reagent.LastModifiedBy
            };

            //var itemsWhereReagentWasUsed = new PrepListItemRepository().Get().ToList()
            //    .Join(new IntermediateStandardRepository().Get(),
            //        prepListItem => prepListItem.PrepList.PrepListId,
            //        intStandard => intStandard.PrepList.PrepListId,
            //        (prepListItem, intStandard) => new { prepListItem.PrepList, intStandard.PrepList.PrepListId })
            //    .Where(x => x.PrepList.PrepListId == x.PrepListId)
            //    //.Join(new StockReagentRepository().Get(),
            //    //    prepListItem => prepListItem.PrepList.PrepListId,
            //    //    linqReagent => linqReagent.)
            //    .GroupBy(x => x.PrepList.IntermediateStandards)
            //    .Select(x => x as IntermediateStandard)
            //    .ToList();

            /* CONSIDER STORED PROCEDURE HERE */
            
            foreach (var invItem in reagent.InventoryItems) {
                if (reagent.ReagentId == invItem.StockReagent.ReagentId) {
                    vReagent.Amount = invItem.Amount;
                    vReagent.CaseNumber = invItem.CaseNumber;
                    vReagent.CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.UsedFor = invItem.UsedFor;
                    vReagent.Unit = invItem.Unit;
                    vReagent.CatalogueCode = invItem.CatalogueCode;
                    vReagent.Grade = invItem.Grade;
                    //vReagent.ItemsWhereReagentUsed = itemsWhereReagentWasUsed;
                    //vReagent.PrepListItems = new PrepListItemRepository().Get().Where(x => x.StockReagent != null && x.StockReagent.ReagentId == reagent.ReagentId).ToList();
                }
            }

            return View(vReagent);
        }

        // GET: /Reagent/Create
        [Route("Reagent/Create")]
        public ActionResult Create() {
            var volumeUnits = new UnitRepository().Get().Where(item => item.UnitType.Equals("Volume")).ToList();
            var weightUnits = new UnitRepository().Get().Where(item => item.UnitType.Equals("Weight")).ToList();

            ViewBag.WeightUnits = weightUnits;
            ViewBag.VolumeUnits = volumeUnits;
            return View();
        }

        // POST: /Reagent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Reagent/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatalogueCode,IdCode,DateEntered,DateCreated,DateModified,ReagentName,CaseNumber,Amount,Grade,UsedFor,InventoryItemName")] StockReagentViewModel model, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS, string submit) {
            int? selectedValue = Convert.ToInt32(Request.Form["Unit"]);
            model.Unit = new UnitRepository().Get(selectedValue);
            model.EnteredBy = string.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name) 
                                ? "USERID"
                                : System.Web.HttpContext.Current.User.Identity.Name;

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
                    Unit = model.Unit,
                    Type = model.GetType().Name
                };
                inventoryItem.MSDS.Add(model.MSDS);
                inventoryItem.CertificatesOfAnalysis.Add(model.CertificateOfAnalysis);
                reagent.InventoryItems.Add(inventoryItem);

                repo.Create(reagent);

                if (!string.IsNullOrEmpty(submit) && submit.Equals("Save")) {
                    //save pressed
                    return RedirectToAction("Index");
                } else {
                    //save & new pressed
                    return RedirectToAction("Create");
                }
            }
            return View(model);
        }

        // GET: /Reagent/Edit/5
        [Route("Reagent/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockReagent stockreagent = repo.Get(id);
            if (stockreagent == null) {
                return HttpNotFound();
            }
            return View(stockreagent);
        }

        // POST: /Reagent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Reagent/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReagentId,IdCode,DateEntered,ReagentName,EnteredBy")] StockReagent stockreagent) {
            if (ModelState.IsValid) {
                repo.Update(stockreagent);
                return RedirectToAction("Index");
            }
            return View(stockreagent);
        }

        // GET: /Reagent/Delete/5
        [Route("Reagent/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockReagent stockreagent = repo.Get(id);
            if (stockreagent == null) {
                return HttpNotFound();
            }
            return View(stockreagent);
        }

        // POST: /Reagent/Delete/5
        [Route("Reagent/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
