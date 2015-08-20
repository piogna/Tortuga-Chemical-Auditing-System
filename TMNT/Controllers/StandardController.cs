using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;

namespace TMNT.Controllers {
    public class StandardController : Controller {

        private IRepository<StockStandard> repoStandard;
        public StandardController()
            : this(new StockStandardRepository()) {
        }

        public StandardController(IRepository<StockStandard> repoStandard) {
            this.repoStandard = repoStandard;
        }

        [Route("Standard")]
        // GET: /Standard/
        public ActionResult Index() {
            var standards = repoStandard.Get();
            List<StockStandardViewModel> list = new List<StockStandardViewModel>();

            foreach (var item in standards) {
                list.Add(new StockStandardViewModel() {
                    StockStandardId = item.StockStandardId,
                    StockStandardName = item.StockStandardName,
                    DateEntered = item.DateEntered,
                    EnteredBy = item.EnteredBy,
                    IdCode = item.IdCode,
                    LastModified = item.LastModified,
                    LastModifiedBy = item.LastModifiedBy,
                    LowAmountThreshHold = item.LowAmountThreshHold,
                    Purity = item.Purity,
                    SolventUsed = item.SolventUsed
                });
            }
            //iterating through the associated InventoryItem and retrieving the appropriate data
            //this is faster than LINQ
            int counter = 0;
            foreach (var standard in standards) {
                foreach (var invItem in standard.InventoryItems) {
                    if (standard.StockStandardId == invItem.StockStandard.StockStandardId) {
                        list[counter].Amount = invItem.Amount;
                        list[counter].CaseNumber = invItem.CaseNumber;
                        list[counter].CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].UsedFor = invItem.UsedFor;
                        list[counter].Unit = invItem.Unit;
                        list[counter].CatalogueCode = invItem.CatalogueCode;
                        list[counter].Grade = invItem.Grade;
                    }
                }
                counter++;
            }
            return View(list);
        }

        // GET: /Standard/Details/5
        [Route("Standard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (Request.UrlReferrer.AbsolutePath.Contains("IntermediateStandard")) {
                ViewBag.ReturnUrl = Request.UrlReferrer.AbsolutePath;
            }

            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StockStandard standard = repoStandard.Get(id);

            StockStandardViewModel vStandard = new StockStandardViewModel() {
                StockStandardId = standard.StockStandardId,
                IdCode = standard.IdCode,
                DateEntered = standard.DateEntered,
                EnteredBy = standard.EnteredBy,
                StockStandardName = standard.StockStandardName,
                LowAmountThreshHold = standard.LowAmountThreshHold,
                LastModified = standard.LastModified,
                LastModifiedBy = standard.LastModifiedBy
            };

            foreach (var invItem in standard.InventoryItems) {
                if (invItem.StockStandard.StockStandardId == standard.StockStandardId) {
                    vStandard.Amount = invItem.Amount;
                    vStandard.CaseNumber = invItem.CaseNumber;
                    vStandard.CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vStandard.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vStandard.UsedFor = invItem.UsedFor;
                    vStandard.Unit = invItem.Unit;
                    vStandard.CatalogueCode = invItem.CatalogueCode;
                    vStandard.Grade = invItem.Grade;
                }
            }

            if (standard == null) {
                return HttpNotFound();
            }
            return View(vStandard);
        }

        [Route("Standard/Create")]
        // GET: /Standard/Create
        public ActionResult Create() {
            var volumeUnits = new UnitRepository().Get().Where(item => item.UnitType.Equals("Volume")).ToList();
            var weightUnits = new UnitRepository().Get().Where(item => item.UnitType.Equals("Weight")).ToList();

            ViewBag.WeightUnits = weightUnits;
            ViewBag.VolumeUnits = volumeUnits;
            return View();
        }

        // POST: /Standard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Standard/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCode,StockStandardName,CatalogueCode,InventoryItemName,Amount,Grade,UsedFor,CaseNumber,SolventUsed,Purity")] StockStandardViewModel model, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS, string submit) {
            int? selectedValue = Convert.ToInt32(Request.Form["Unit"]);
            model.Unit = new UnitRepository().Get(selectedValue);

            if (ModelState.IsValid) {
                if (uploadCofA != null) {
                    var cofa = new CertificateOfAnalysis() {
                        FileName = uploadCofA.FileName,
                        ContentType = uploadCofA.ContentType,
                        DateAdded = DateTime.Now
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
                        DateAdded = DateTime.Now
                    };
                    using (var reader = new System.IO.BinaryReader(uploadMSDS.InputStream)) {
                        msds.Content = reader.ReadBytes(uploadMSDS.ContentLength);
                    }
                    model.MSDS = msds;
                }

                StockStandard standard = new StockStandard() {
                    IdCode = model.IdCode,
                    StockStandardName = model.StockStandardName,
                    DateEntered = DateTime.Today,
                    SolventUsed = model.SolventUsed,
                    Purity = model.Purity,
                    EnteredBy = string.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name)
                                ? "USERID"
                                : System.Web.HttpContext.Current.User.Identity.Name
                };

                InventoryItem inventoryItem = new InventoryItem() {
                    CatalogueCode = model.CatalogueCode,
                    Amount = model.Amount,
                    Grade = model.Grade,
                    CaseNumber = model.CaseNumber,
                    UsedFor = model.UsedFor,
                    CreatedBy = string.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name) ? System.Web.HttpContext.Current.User.Identity.Name : "USERID",
                    DateCreated = DateTime.Today,
                    DateModified = DateTime.Today,
                };
                inventoryItem.MSDS.Add(model.MSDS);
                inventoryItem.CertificatesOfAnalysis.Add(model.CertificateOfAnalysis);
                standard.InventoryItems.Add(inventoryItem);

                repoStandard.Create(standard);

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

        // GET: /Standard/Edit/5
        [Route("Standard/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            //hard-coded right now for prototype purposes
            //id = 1;
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockStandard stockstandard = repoStandard.Get(id);

            StockStandardViewModel model = new StockStandardViewModel() {
                StockStandardName = stockstandard.StockStandardName,
                DateEntered = stockstandard.DateEntered,
                IdCode = stockstandard.IdCode,
                Purity = stockstandard.Purity,
                SolventUsed = stockstandard.SolventUsed,
                CertificateOfAnalysis = stockstandard.InventoryItems.Where(x => x.StockStandard.StockStandardId == stockstandard.StockStandardId).Select(x => x.CertificatesOfAnalysis.OrderBy(y => y.DateAdded).First()).First()
            };

            foreach (var item in stockstandard.InventoryItems) {
                model.Amount = item.Amount;
                model.Grade = item.Grade;
                model.CatalogueCode = item.CatalogueCode;
                model.CaseNumber = item.CaseNumber;
                model.UsedFor = item.UsedFor;
            }

            if (stockstandard == null) {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Standard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Standard/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Amount,CertificateOfAnalysis")] StockStandardViewModel stockstandard, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS) {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid) {
                StockStandard standard = new StockStandard() {
                    IdCode = stockstandard.IdCode,
                    Purity = stockstandard.Purity
                };
                repoStandard.Update(standard);
                return View();
            }
            return View(stockstandard);
        }

        // GET: /Standard/Delete/5
        [Route("Standard/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockStandard stockstandard = repoStandard.Get(id);
            if (stockstandard == null) {
                return HttpNotFound();
            }
            return View(stockstandard);
        }

        // POST: /Standard/Delete/5
        [Route("Standard/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            repoStandard.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
