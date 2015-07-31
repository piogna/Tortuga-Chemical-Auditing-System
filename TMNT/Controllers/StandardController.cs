using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using Microsoft.AspNet.Identity;
using TMNT.Models.FakeRepository;


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
            return View(repoStandard.Get());//db.StockStandards.ToList());
        }

        // GET: /Standard/Details/5
        [Route("Standard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockStandard stockstandard = repoStandard.Get(id);//db.StockStandards.Find(id);
            if (stockstandard == null) {
                return HttpNotFound();
            }
            return View(stockstandard);
        }

        [Route("Standard/Create")]
        // GET: /Standard/Create
        public ActionResult Create() {
            var units = new UnitRepository().Get().ToList();
            SelectList list = new SelectList(units, "UnitId", "UnitName");
            ViewBag.Units = list;
            return View();
        }

        // POST: /Standard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Standard/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCode,StockStandardName,CatalogueCode,InventoryItemName,Amount,Grade,UsedFor,CaseNumber,SolventUsed,Purity")] InventoryStockStandardViewModel model, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS, string submit) {
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
                        DateAdded = DateTime.Today
                    };
                    using (var reader = new System.IO.BinaryReader(uploadCofA.InputStream)) {
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
                    return RedirectToAction("Index");// View("Index");
                } else {
                    //save & new pressed
                    return RedirectToAction("Create");
                }
                //return View("Confirmation", model);
            }
            return View(model);
        }

        // GET: /Standard/Edit/5
        [Route("Standard/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            //hard-coded right now for prototype purposes
            id = 1;
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockStandard stockstandard = new FakeStockStandardRepository().Get(id);//repoStandard.Get(id);

            InventoryStockStandardViewModel model = new InventoryStockStandardViewModel() {
                StockStandardName = stockstandard.StockStandardName,
                DateEntered = stockstandard.DateEntered,
                IdCode = stockstandard.IdCode,
                Purity = stockstandard.Purity,
                SolventUsed = stockstandard.SolventUsed
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
        public ActionResult Edit([Bind(Include = "StockStandardName,DateEntered,IdCode,Purity,SolventUsed,Size,Grade,CatalogueCode,CaseNumber,UsedFor")] 
            InventoryStockStandardViewModel stockstandard) {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid) {
                //db.Entry(stockstandard).State = EntityState.Modified;
                //db.SaveChanges();
                //repoStandard.Update(stockstandard);
                StockStandard standard = new StockStandard() {
                    IdCode = stockstandard.IdCode,
                    Purity = stockstandard.Purity,
                    InventoryItems = new List<InventoryItem>() {
                        new InventoryItem() { 
                            Grade = stockstandard.Grade, Amount = stockstandard.Amount, 
                            CatalogueCode = stockstandard.CatalogueCode
                        }
                    }
                };

                //foreach (var item in standard.InventoryItems) {
                //    item.CatalogueCode = stockstandard.CatalogueCode;
                //    item.Size = stockstandard.Size;
                //    item.Grade = stockstandard.Grade;
                //}

                new FakeStockStandardRepository().Update(standard);
                return View("Confirmation", stockstandard);
            }
            return View(stockstandard);
        }

        // GET: /Standard/Delete/5
        [Route("Standard/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockStandard stockstandard = repoStandard.Get(id);//db.StockStandards.Find(id);
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
            //StockStandard stockstandard = db.StockStandards.Find(id);
            //db.StockStandards.Remove(stockstandard);
            //db.SaveChanges();
            repoStandard.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
