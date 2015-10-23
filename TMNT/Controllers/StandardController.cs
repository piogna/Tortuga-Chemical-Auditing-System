using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;
using TMNT.Helpers;
using TMNT.Models.Enums;
using TMNT.Filters;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class StandardController : Controller {

        private IRepository<StockStandard> repo;
        public StandardController()
            : this(new StockStandardRepository(DbContextSingleton.Instance)) {
        }

        public StandardController(IRepository<StockStandard> repoStandard) {
            repo = repoStandard;
        }

        [Route("Standard")]
        // GET: /Standard/
        public ActionResult Index() {
            var userDepartment = HelperMethods.GetUserDepartment();
            List<StockStandardIndexViewModel> lStandards = new List<StockStandardIndexViewModel>();
            List<InventoryItem> invRepo = null;

            if (userDepartment.DepartmentName.Equals("Quality Assurance")) {
                invRepo = new InventoryItemRepository().Get().ToList();
            } else {
                invRepo = new InventoryItemRepository().Get().Where(item => item.Department.DepartmentName.Equals(userDepartment.DepartmentName)).ToList();
            }

            foreach (var item in invRepo) {
                if (item.StockStandard != null) {
                    lStandards.Add(new StockStandardIndexViewModel() {
                        StockStandardId = item.StockStandard.StockStandardId,
                        CreatedBy = item.CreatedBy,
                        DateCreated = item.DateCreated,
                        DateOpened = item.DateOpened,
                        ExpiryDate = item.ExpiryDate,
                        IdCode = item.StockStandard.IdCode,
                        LotNumber = item.StockStandard.LotNumber,
                        StockStandardName = item.StockStandard.StockStandardName,
                        IsExpired = item.ExpiryDate < DateTime.Today,
                        IsExpiring = item.ExpiryDate < DateTime.Today.AddDays(30) && !(item.ExpiryDate < DateTime.Today)
                    });
                }
            }

            //old Standard Index code. DO NOT DELETE FOR REFERENCE.

            //var standards = repoStandard.Get();
            //List<StockStandardIndexViewModel> list = new List<StockStandardIndexViewModel>();

            //foreach (var item in standards) {
            //    list.Add(new StockStandardIndexViewModel() {
            //        StockStandardId = item.StockStandardId,
            //        LotNumber = item.LotNumber,
            //        StockStandardName = item.StockStandardName,
            //        IdCode = item.IdCode
            //    });
            //}
            ////iterating through the associated InventoryItem and retrieving the appropriate data
            ////this is faster than LINQ
            //int counter = 0;
            //foreach (var standard in standards) {
            //    foreach (var invItem in standard.InventoryItems) {
            //        if (standard.StockStandardId == invItem.StockStandard.StockStandardId) {
            //            list[counter].ExpiryDate = invItem.ExpiryDate;
            //            list[counter].DateOpened = invItem.DateOpened;
            //            list[counter].DateCreated = invItem.DateCreated;
            //            list[counter].CreatedBy = invItem.CreatedBy;
            //            list[counter].DateModified = invItem.DateModified;
            //        }
            //    }
            //    counter++;
            //}
            return View(lStandards);
        }

        // GET: /Standard/Details/5
        [Route("Standard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StockStandard standard = repo.Get(id);

            if (standard == null) {
                return HttpNotFound("The standard requested does not exist.");
            }

            if (Request.UrlReferrer.AbsolutePath.Contains("IntermediateStandard")) {
                ViewBag.ReturnUrl = Request.UrlReferrer.AbsolutePath;
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
                    //vStandard.AllMSDS = invItem.MSDS.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).ToList();
                    vStandard.MSDSNotes = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First().MSDSNotes;
                    vStandard.SolventSupplierName = invItem.SupplierName;
                    vStandard.SupplierName = invItem.SupplierName;
                    vStandard.IsExpired = invItem.ExpiryDate < DateTime.Today;
                    vStandard.IsExpiring = invItem.ExpiryDate < DateTime.Today.AddDays(30) && !(invItem.ExpiryDate < DateTime.Today);
                    vStandard.NumberOfBottles = invItem.NumberOfBottles;
                }
            }
            return View(vStandard);
        }

        [Route("Standard/Create")]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor,Quality Assurance")]
        // GET: /Standard/Create
        public ActionResult Create() {
            var model = new StockStandardCreateViewModel();
            SetStockStandard(model);

            return View(model);
        }

        // POST: /Standard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Standard/Create")]
        [ValidateAntiForgeryToken]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor,Quality Assurance")]
        public ActionResult Create([Bind(Include = "StockStandardName,SolventSupplierName,SupplierName,CatalogueCode,StorageRequirements,MSDSNotes,LotNumber,ExpiryDate,MSDSNotes,UsedFor,SolventUsed,Purity,NumberOfBottles,InitialAmount,Concentration,DateReceived")]
                    StockStandardCreateViewModel model, string[] AmountUnit, string[] ConcentrationUnit, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS, string submit) {
            //model isn't valid, return to the form
            if (!ModelState.IsValid) {
                SetStockStandard(model);
                return View(model);
            }

            //last line of defense for number of bottles
            if (model.NumberOfBottles == 0) { model.NumberOfBottles = 1; }

            var devicesUsed = Request.Form["Devices"];
            var deviceRepo = new DeviceRepository();
            var user = HelperMethods.GetCurrentUser();
            var department = HelperMethods.GetUserDepartment();
            var numOfItems = new InventoryItemRepository().Get().Count();

            if (devicesUsed == null) {
                ModelState.AddModelError("", "You must select a device that was used.");
                SetStockStandard(model);
                return View(model);
            }

            if (devicesUsed.Contains(",")) {
                model.DeviceOne = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
                model.DeviceTwo = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[1])).FirstOrDefault();
            } else {
                model.DeviceOne = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
            }

            model.InitialAmountUnits = AmountUnit[0];
            model.InitialConcentrationUnits = ConcentrationUnit[0];

            if (AmountUnit.Length > 1) {
                model.InitialAmountUnits += "/" + AmountUnit[1];
            }

            if (ConcentrationUnit.Length > 1) {
                model.InitialConcentrationUnits += "/" + ConcentrationUnit[1];
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

            InventoryItem inventoryItem = new InventoryItem() {
                CatalogueCode = model.CatalogueCode.ToUpper(),
                Department = department,
                UsedFor = model.UsedFor,
                CreatedBy = user.UserName,
                ExpiryDate = model.ExpiryDate,
                DateReceived = model.DateReceived,
                DateCreated = DateTime.Today,
                DateModified = null,
                Type = "Standard",
                FirstDeviceUsed = model.DeviceOne,
                SecondDeviceUsed = model.DeviceTwo,
                StorageRequirements = model.StorageRequirements,
                SupplierName = model.SupplierName,
                NumberOfBottles = model.NumberOfBottles,
                InitialAmount = model.InitialAmount.ToString() + " " + model.InitialAmountUnits 
            };

            inventoryItem.MSDS.Add(model.MSDS);
            inventoryItem.CertificatesOfAnalysis.Add(model.CertificateOfAnalysis);

            StockStandard createStandard = null;
            CheckModelState result = CheckModelState.Invalid;//default to invalid to expect the worst

            if (model.NumberOfBottles > 1) {
                for (int i = 1; i <= model.NumberOfBottles; i++) {
                    createStandard = new StockStandard() {
                        IdCode = department.Location.LocationCode + "-" + (numOfItems + 1) + "-" + model.LotNumber + "/" + i,//append number of bottles
                        LotNumber = model.LotNumber,
                        StockStandardName = model.StockStandardName,
                        Purity = model.Purity,
                        SolventUsed = model.SolventUsed,
                        SolventSupplierName = model.SolventSupplierName,
                        Concentration = model.Concentration.ToString() + " " 
                    };

                    createStandard.InventoryItems.Add(inventoryItem);
                    result = repo.Create(createStandard);

                    //creation wasn't successful - break from loop and let switch statement handle the problem
                    if (result != CheckModelState.Valid) { break; }
                }
            } else {
                createStandard = new StockStandard() {
                    IdCode = department.Location.LocationCode + "-" + (numOfItems + 1) + "-" + model.LotNumber,//only 1 bottle, no need to concatenate
                    LotNumber = model.LotNumber,
                    StockStandardName = model.StockStandardName,
                    Purity = model.Purity,
                    SolventUsed = model.SolventUsed,
                    SolventSupplierName = model.SolventSupplierName,
                    Concentration = model.Concentration.ToString() + " " + model.InitialConcentrationUnits
                };

                createStandard.InventoryItems.Add(inventoryItem);
                result = repo.Create(createStandard);
            }

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The creation of " + createStandard.StockStandardName + " failed. Please double check all inputs and try again.");
                    SetStockStandard(model);
                    return View(model);
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    SetStockStandard(model);
                    return View(model);
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    SetStockStandard(model);
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
                    SetStockStandard(model);
                    return View(model);
            }
        }

        // GET: /Standard/Edit/5
        [Route("Standard/Edit/{id?}")]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StockStandard stockstandard = repo.Get(id);

            if (stockstandard == null) {
                return HttpNotFound();
            }

            StockStandardEditViewModel model = new StockStandardEditViewModel() {
                StockStandardId = stockstandard.StockStandardId,
                LotNumber = stockstandard.LotNumber,
                StockStandardName = stockstandard.StockStandardName,
                IdCode = stockstandard.IdCode,
                CertificateOfAnalysis = stockstandard.InventoryItems.Where(x => x.StockStandard.StockStandardId == stockstandard.StockStandardId).Select(x => x.CertificatesOfAnalysis.OrderBy(y => y.DateAdded).First()).First(),
                MSDS = stockstandard.InventoryItems.Where(x => x.StockStandard.StockStandardId == stockstandard.StockStandardId).Select(x => x.MSDS.OrderBy(y => y.DateAdded).First()).First()
            };

            foreach (var item in stockstandard.InventoryItems) {
                model.DateCreated = item.DateCreated;
                model.SupplierName = item.SupplierName;
                model.ExpiryDate = item.ExpiryDate;
            }
            return View(model);
        }

        // POST: /Standard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Standard/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor")]
        public ActionResult Edit([Bind(Include = "LotNumber,StockStandardId,ExpiryDate,IdCode,SupplierName,StockStandardName")] StockStandardEditViewModel stockstandard, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS) {
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

                repo.Update(updateStandard);

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
                invItem.ExpiryDate = stockstandard.ExpiryDate;

                inventoryRepo.Update(invItem);

                return RedirectToAction("Index");
            }
            return View(stockstandard);
        }

        // GET: /Standard/Delete/5
        [Route("Standard/Delete/{id?}")]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockStandard stockstandard = repo.Get(id);
            if (stockstandard == null) {
                return HttpNotFound();
            }
            return View(stockstandard);
        }

        // POST: /Standard/Delete/5
        [Route("Standard/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor")]
        public ActionResult DeleteConfirmed(int id) {
            repo.Delete(id);
            return RedirectToAction("Index");
        }

        public StockStandardCreateViewModel SetStockStandard(StockStandardCreateViewModel model) {
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
