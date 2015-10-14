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
using TMNT.Utils;
using TMNT.Helpers;
using TMNT.Models.Enums;
using TMNT.Filters;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class ReagentController : Controller {

        private IRepository<StockReagent> repo;

        public ReagentController() : this(new StockReagentRepository(DbContextSingleton.Instance)) { }

        public ReagentController(IRepository<StockReagent> repo) {
            this.repo = repo;
        }
        // GET: /Reagent/
        [Route("Reagent")]
        public ActionResult Index() {
            var userDepartment = HelperMethods.GetUserDepartment();
            List<StockReagentIndexViewModel> lReagents = new List<StockReagentIndexViewModel>();

            var invRepo = new InventoryItemRepository().Get()
                .ToList();
            
            foreach (var item in invRepo) {
                if (item.StockReagent != null && item.Department == userDepartment) {
                    lReagents.Add(new StockReagentIndexViewModel() {
                        ReagentId = item.StockReagent.ReagentId,
                        CreatedBy = item.CreatedBy,
                        DateCreated = item.DateCreated,
                        DateOpened = item.DateOpened,
                        ExpiryDate = item.ExpiryDate,
                        IdCode = item.StockReagent.IdCode,
                        LotNumber = item.StockReagent.LotNumber,
                        ReagentName = item.StockReagent.ReagentName
                    });
                }
            }

            //old Reagent Index code. DO NOT DELETE FOR REFERENCE.

            //var reagents = repo.Get();

            //List<StockReagentIndexViewModel> list = new List<StockReagentIndexViewModel>();

            //foreach (var item in reagents) {
            //    list.Add(new StockReagentIndexViewModel() {
            //        ReagentId = item.ReagentId,
            //        LotNumber = item.LotNumber,
            //        IdCode = item.IdCode,
            //        ReagentName = item.ReagentName
            //    });
            //}

            ////iterating through the associated InventoryItem and retrieving the appropriate data
            ////this is faster than LINQ
            //int counter = 0;
            //foreach (var reagent in reagents) {
            //    foreach (var invItem in reagent.InventoryItems) {
            //        if (reagent.ReagentId == invItem.StockReagent.ReagentId) {
            //            list[counter].ExpiryDate = invItem.ExpiryDate;
            //            list[counter].DateOpened = invItem.DateOpened;
            //            list[counter].DateCreated = invItem.DateCreated;
            //            list[counter].CreatedBy = invItem.CreatedBy;
            //        }
            //    }
            //    counter++;
            //}
            return View(lReagents);
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

            var vReagent = new StockReagentDetailsViewModel() {
                ReagentId = reagent.ReagentId,
                IdCode = reagent.IdCode,
                ReagentName = reagent.ReagentName,
                LastModifiedBy = reagent.LastModifiedBy
            };

            foreach (var invItem in reagent.InventoryItems) {
                if (reagent.ReagentId == invItem.StockReagent.ReagentId) {
                    vReagent.DateCreated = invItem.DateCreated;
                    vReagent.DateModified = invItem.DateModified;
                    vReagent.CreatedBy = invItem.CreatedBy;
                    vReagent.ExpiryDate = invItem.ExpiryDate;
                    vReagent.CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.UsedFor = invItem.UsedFor;
                    vReagent.Unit = invItem.Unit;
                    vReagent.Department = invItem.Department;
                    vReagent.CatalogueCode = invItem.CatalogueCode;
                    vReagent.Grade = invItem.Grade;
                    vReagent.GradeAdditionalNotes = invItem.GradeAdditionalNotes;
                    vReagent.AllCertificatesOfAnalysis = invItem.CertificatesOfAnalysis.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).ToList();
                    vReagent.AllMSDS = invItem.MSDS.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).ToList();
                    vReagent.MSDSNotes = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First().MSDSNotes;
                    vReagent.IsExpired = invItem.ExpiryDate >= DateTime.Today;
                    vReagent.SupplierName = invItem.SupplierName;
                }
            }
            return View(vReagent);
        }

        // GET: /Reagent/Create
        [Route("Reagent/Create")]
        public ActionResult Create() {
            var model = new StockReagentCreateViewModel();
            SetStockReagent(model);

            return View(model);
        }

        // POST: /Reagent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Reagent/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatalogueCode,IdCode,MSDSNotes,SupplierName,ReagentName,StorageRequirements,Grade,UsedFor,LotNumber,GradeAdditionalNotes")] StockReagentCreateViewModel model, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS, string submit) {
            //model isn't valid, return to the form
            if (!ModelState.IsValid) {
                SetStockReagent(model);
                return View(model);
            }

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

            StockReagent reagent = new StockReagent() {
                IdCode = model.IdCode,
                LotNumber = model.LotNumber,
                ReagentName = model.ReagentName
            };

            InventoryItem inventoryItem = new InventoryItem() {
                CatalogueCode = model.CatalogueCode,
                Department = HelperMethods.GetUserDepartment(),
                Grade = model.Grade,
                GradeAdditionalNotes = model.GradeAdditionalNotes,
                ExpiryDate = DateTime.Today,
                DateOpened = null,
                DateModified = null,
                DateCreated = DateTime.Today,
                UsedFor = model.UsedFor,
                CreatedBy = !string.IsNullOrEmpty(HelperMethods.GetCurrentUser().UserName)
                            ? HelperMethods.GetCurrentUser().UserName
                            : "USERID",
                Type = "Reagent",
                StorageRequirements = model.StorageRequirements,
                SupplierName = model.SupplierName
            };

            inventoryItem.MSDS.Add(model.MSDS);
            inventoryItem.CertificatesOfAnalysis.Add(model.CertificateOfAnalysis);
            reagent.InventoryItems.Add(inventoryItem);
            //getting the enum result
            var result = repo.Create(reagent);

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The creation of " + reagent.ReagentName + " failed. Please double check all inputs and try again.");
                    SetStockReagent(model);
                    return View(model);
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    SetStockReagent(model);
                    return View(model);
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    SetStockReagent(model);
                    return View(model);
                case CheckModelState.Valid:
                    if (!string.IsNullOrEmpty(submit) && submit.Equals("Save")) {
                        //save pressed
                        return RedirectToAction("Index");
                    } else {
                        //save & new pressed
                        return RedirectToAction("Create");
                    }
                default:
                    ModelState.AddModelError("", "An unknown error occurred.");
                    SetStockReagent(model);
                    return View(model);
            }
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

            StockReagentEditViewModel model = new StockReagentEditViewModel() {
                ReagentId = stockreagent.ReagentId,
                LotNumber = stockreagent.LotNumber,
                ReagentName = stockreagent.ReagentName,
                IdCode = stockreagent.IdCode,
                CertificateOfAnalysis = stockreagent.InventoryItems.Where(x => x.StockReagent.ReagentId == stockreagent.ReagentId).Select(x => x.CertificatesOfAnalysis.OrderBy(y => y.DateAdded).First()).First(),
                MSDS = stockreagent.InventoryItems.Where(x => x.StockReagent.ReagentId == stockreagent.ReagentId).Select(x => x.MSDS.OrderBy(y => y.DateAdded).First()).First()
            };

            foreach (var item in stockreagent.InventoryItems) {
                model.Grade = item.Grade;
                model.GradeAdditionalNotes = item.GradeAdditionalNotes;
                model.ExpiryDate = item.ExpiryDate;
                model.SupplierName = item.SupplierName;
            }
            return View(model);
        }

        // POST: /Reagent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Reagent/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReagentId,LotNumber,ExpiryDate,SupplierName,ReagentName,IdCode,Grade,GradeAdditionalNotes")] StockReagentEditViewModel stockreagent, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS) {


            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid) {
                var invRepo = new InventoryItemRepository(DbContextSingleton.Instance);

                InventoryItem invItem = invRepo.Get()
                        .Where(item => item.StockReagent != null && item.StockReagent.ReagentId == stockreagent.ReagentId)
                        .FirstOrDefault();

                StockReagent updateReagent = invItem.StockReagent;
                updateReagent.LotNumber = stockreagent.LotNumber;
                updateReagent.LastModifiedBy = !string.IsNullOrEmpty(HelperMethods.GetCurrentUser().UserName) ? HelperMethods.GetCurrentUser().UserName : "USERID";

                repo.Update(updateReagent);

                if (uploadCofA != null) {
                    var cofa = new CertificateOfAnalysis() {
                        FileName = uploadCofA.FileName,
                        ContentType = uploadCofA.ContentType,
                        DateAdded = DateTime.Today
                    };

                    using (var reader = new System.IO.BinaryReader(uploadCofA.InputStream)) {
                        cofa.Content = reader.ReadBytes(uploadCofA.ContentLength);
                    }
                    stockreagent.CertificateOfAnalysis = cofa;

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
                    stockreagent.MSDS = msds;
                    invItem.MSDS.Add(msds);
                }

                invItem.DateModified = DateTime.Today;
                invItem.SupplierName = stockreagent.SupplierName;
                invItem.Grade = stockreagent.Grade;
                invItem.GradeAdditionalNotes = stockreagent.GradeAdditionalNotes;
                invItem.ExpiryDate = stockreagent.ExpiryDate;
                invRepo.Update(invItem);

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

        public StockReagentCreateViewModel SetStockReagent(StockReagentCreateViewModel model) {
            var units = new UnitRepository(DbContextSingleton.Instance).Get();
            var devices = new DeviceRepository(DbContextSingleton.Instance).Get().ToList();

            model.WeightUnits = units.Where(item => item.UnitType.Equals("Weight")).ToList();
            model.VolumetricUnits = units.Where(item => item.UnitType.Equals("Volume")).ToList();
            model.BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance")).ToList();
            model.VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric")).ToList();

            return model;
        }
    }
}
