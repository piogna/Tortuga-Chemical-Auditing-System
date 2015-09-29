using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;
using TMNT.Helpers;

namespace TMNT.Controllers {
    [Authorize]
    public class StandardController : Controller {

        private IRepository<StockStandard> repoStandard;
        public StandardController()
            : this(new StockStandardRepository(DbContextSingleton.Instance)) {
        }

        public StandardController(IRepository<StockStandard> repoStandard) {
            this.repoStandard = repoStandard;
        }

        [Route("Standard")]
        // GET: /Standard/
        public ActionResult Index() {
            var standards = repoStandard.Get();
            List<StockStandardIndexViewModel> list = new List<StockStandardIndexViewModel>();

            foreach (var item in standards) {
                list.Add(new StockStandardIndexViewModel() {
                    StockStandardId = item.StockStandardId,
                    LotNumber = item.LotNumber,
                    StockStandardName = item.StockStandardName,
                    IdCode = item.IdCode
                });
            }
            //iterating through the associated InventoryItem and retrieving the appropriate data
            //this is faster than LINQ
            int counter = 0;
            foreach (var standard in standards) {
                foreach (var invItem in standard.InventoryItems) {
                    if (standard.StockStandardId == invItem.StockStandard.StockStandardId) {
                        list[counter].ExpiryDate = invItem.ExpiryDate;
                        list[counter].DateOpened = invItem.DateOpened;
                        list[counter].DateCreated = invItem.DateCreated;
                        list[counter].CreatedBy = invItem.CreatedBy;
                        list[counter].DateModified = invItem.DateModified;
                    }
                }
                counter++;
            }
            return View(list);
        }

        // GET: /Standard/Details/5
        [Route("Standard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StockStandard standard = repoStandard.Get(id);

            if (standard == null) {
                return HttpNotFound("The standard requested does not exist.");
            }

            StockStandardDetailsViewModel vStandard = new StockStandardDetailsViewModel() {
                StockStandardId = standard.StockStandardId,
                LotNumber = standard.LotNumber,
                IdCode = standard.IdCode,
                StockStandardName = standard.StockStandardName,
                LastModifiedBy = standard.LastModifiedBy,
                SolventUsed = standard.SolventUsed
            };

            foreach (var invItem in standard.InventoryItems) {
                if (invItem.StockStandard.StockStandardId == standard.StockStandardId) {
                    vStandard.ExpiryDate = invItem.ExpiryDate;
                    vStandard.DateOpened = invItem.DateOpened;
                    vStandard.DateCreated = invItem.DateCreated;
                    vStandard.CreatedBy = invItem.CreatedBy;
                    vStandard.DateModified = invItem.DateModified;
                    vStandard.CertificateOfAnalysis = invItem.CertificatesOfAnalysis.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vStandard.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vStandard.UsedFor = invItem.UsedFor;
                    vStandard.Unit = invItem.Unit;
                    vStandard.Department = invItem.Department;
                    vStandard.CatalogueCode = invItem.CatalogueCode;
                    vStandard.AllCertificatesOfAnalysis = invItem.CertificatesOfAnalysis.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).ToList();
                    vStandard.AllMSDS = invItem.MSDS.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).ToList();
                    vStandard.MSDSNotes = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First().MSDSNotes;
                    vStandard.SolventSupplierName = invItem.SupplierName;
                    vStandard.SupplierName = invItem.SupplierName;
                }
            }
            return View(vStandard);
        }

        [Route("Standard/Create")]
        // GET: /Standard/Create
        public ActionResult Create() {
            var model = new StockStandardCreateViewModel();
            var devices = new DeviceRepository(DbContextSingleton.Instance).Get();

            var balanceDevices = devices.Where(item => item.DeviceType.Equals("Balance"));
            var volumeDevices = devices.Where(item => item.DeviceType.Equals("Volumetric"));

            var storageRequirements = new List<string>() { "Fridge", "Freezer", "Shelf" };
            
            model.Storage = storageRequirements;

            model.BalanceDevices = balanceDevices;
            model.VolumeDevices = volumeDevices;
            return View(model);
        }

        // POST: /Standard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Standard/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCode,StockStandardName,SolventSupplierName,SupplierName,CatalogueCode,StorageRequirements,MSDSNotes,LotNumber,ExpiryDate,MSDSNotes,UsedFor,SolventUsed,Purity")]
                    StockStandardCreateViewModel model, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS, string submit) {
            
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
                        DateAdded = DateTime.Today,
                        MSDSNotes = model.MSDSNotes
                    };
                    using (var reader = new System.IO.BinaryReader(uploadMSDS.InputStream)) {
                        msds.Content = reader.ReadBytes(uploadMSDS.ContentLength);
                    }
                    model.MSDS = msds;
                }

                StockStandard standard = new StockStandard() {
                    IdCode = model.IdCode,
                    LotNumber = model.LotNumber,
                    StockStandardName = model.StockStandardName,
                    SolventUsed = model.SolventUsed,
                    Purity = model.Purity,
                    SolventSupplierName = model.SolventSupplierName
                };

                InventoryItem inventoryItem = new InventoryItem() {
                    CatalogueCode = model.CatalogueCode,
                    Department = HelperMethods.GetUserDepartment(),
                    ExpiryDate = model.ExpiryDate,
                    UsedFor = model.UsedFor,
                    CreatedBy = !string.IsNullOrEmpty(HelperMethods.GetCurrentUser().UserName) ? HelperMethods.GetCurrentUser().UserName : "USERID",
                    DateCreated = DateTime.Today,
                    DateModified = DateTime.Today,
                    Type = "Standard",
                    StorageRequirements = model.StorageRequirements,
                    SupplierName = model.SupplierName
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
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            StockStandard stockstandard = repoStandard.Get(id);

            if (stockstandard == null) {
                return HttpNotFound();
            }
            
            StockStandardEditViewModel model = new StockStandardEditViewModel() {
                StockStandardId = stockstandard.StockStandardId,
                LotNumber = stockstandard.LotNumber,
                StockStandardName = stockstandard.StockStandardName,
                IdCode = stockstandard.IdCode,
                //Purity = stockstandard.Purity,
                //SolventSupplierName = stockstandard.SolventSupplierName,
                //SolventUsed = stockstandard.SolventUsed,
                CertificateOfAnalysis = stockstandard.InventoryItems.Where(x => x.StockStandard.StockStandardId == stockstandard.StockStandardId).Select(x => x.CertificatesOfAnalysis.OrderBy(y => y.DateAdded).First()).First(),
                MSDS = stockstandard.InventoryItems.Where(x => x.StockStandard.StockStandardId == stockstandard.StockStandardId).Select(x => x.MSDS.OrderBy(y => y.DateAdded).First()).First()
            };

            foreach (var item in stockstandard.InventoryItems) {
                model.DateCreated = item.DateCreated;
                //model.CatalogueCode = item.CatalogueCode;
                //model.ExpiryDate = item.ExpiryDate;
                model.SupplierName = item.SupplierName;
                //model.UsedFor = item.UsedFor;
            }
            return View(model);
        }

        // POST: /Standard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Standard/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LotNumber,StockStandardId,IdCode,SupplierName,StockStandardName")] StockStandardEditViewModel stockstandard, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS) {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid) {
                InventoryItemRepository inventoryRepo = new InventoryItemRepository();

                InventoryItem invItem = inventoryRepo.Get()
                        .Where(item => item.StockStandard != null && item.StockStandard.StockStandardId == stockstandard.StockStandardId)
                        .FirstOrDefault();

                StockStandard updateStandard = invItem.StockStandard;
                updateStandard.StockStandardName = stockstandard.StockStandardName;
                updateStandard.IdCode = stockstandard.IdCode;
                updateStandard.LotNumber = stockstandard.LotNumber;
                updateStandard.LastModifiedBy = !string.IsNullOrEmpty(HelperMethods.GetCurrentUser().UserName) ? HelperMethods.GetCurrentUser().UserName : "USERID";
                
                repoStandard.Update(updateStandard);

                if (uploadCofA != null) {
                    var cofa = new CertificateOfAnalysis() {
                        FileName = uploadCofA.FileName,
                        ContentType = uploadCofA.ContentType,
                        DateAdded = DateTime.Today
                    };

                    using (var reader = new System.IO.BinaryReader(uploadCofA.InputStream)) {
                        cofa.Content = reader.ReadBytes(uploadCofA.ContentLength);
                    }
                    stockstandard.CertificateOfAnalysis = cofa;
                    //update inventory item amount
                    //add certificate analysis

                    invItem.CertificatesOfAnalysis.Add(cofa);
                }
                if (uploadMSDS != null) {
                    var msds = new MSDS() {
                        FileName = uploadMSDS.FileName,
                        ContentType = uploadMSDS.ContentType,
                        DateAdded = DateTime.Today
                    };
                    using (var reader = new System.IO.BinaryReader(uploadMSDS.InputStream)) {
                        msds.Content = reader.ReadBytes(uploadMSDS.ContentLength);
                    }
                    stockstandard.MSDS = msds;

                    invItem.MSDS.Add(msds);
                }
                
                invItem.DateModified = DateTime.Today;
                invItem.SupplierName = stockstandard.SupplierName;

                inventoryRepo.Update(invItem);

                return RedirectToAction("Index");
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
