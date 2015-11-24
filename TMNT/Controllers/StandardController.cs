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
using TMNT.Models.Enums;
using TMNT.Filters;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class StandardController : Controller {

        private UnitOfWork _uow;
        public StandardController()
            : this(new UnitOfWork()) {
        }

        public StandardController(UnitOfWork uow) {
            this._uow = uow;
        }

        [Route("Standard")]
        // GET: /Standard/
        public ActionResult Index() {
            var userDepartment = _uow.GetUserDepartment();
            List<StockStandardIndexViewModel> lStandards = new List<StockStandardIndexViewModel>();
            List<InventoryItem> inventoryItems = null;

            if (userDepartment.DepartmentName.Equals("Quality Assurance")) {
                inventoryItems = _uow.InventoryItemRepository.Get().ToList();
            } else {
                inventoryItems = _uow.InventoryItemRepository.Get().Where(item => item.Department.DepartmentName.Equals(userDepartment.DepartmentName)).ToList();
            }

            foreach (var item in inventoryItems) {
                if (item.StockStandard != null) {
                    lStandards.Add(new StockStandardIndexViewModel() {
                        StockStandardId = item.StockStandard.StockStandardId,
                        CreatedBy = item.StockStandard.CreatedBy,
                        CatalogueCode = item.CatalogueCode,
                        DateCreated = item.StockStandard.DateCreated,
                        DateOpened = item.StockStandard.DateOpened,
                        ExpiryDate = item.StockStandard.ExpiryDate,
                        IdCode = item.StockStandard.IdCode,
                        LotNumber = item.StockStandard.LotNumber,
                        StockStandardName = item.StockStandard.StockStandardName,
                        IsExpired = item.StockStandard.ExpiryDate < DateTime.Today,
                        IsExpiring = item.StockStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(item.StockStandard.ExpiryDate < DateTime.Today)
                    });
                }
            }
            return View(lStandards);
        }

        // GET: /Standard/Details/5
        [Route("Standard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StockStandard standard = _uow.StockStandardRepository.Get(id);

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
                SolventUsed = standard.SolventUsed,
                Concentration = standard.Concentration,
                Purity = standard.Purity,
                ExpiryDate = standard.ExpiryDate,
                DateOpened = standard.DateOpened,
                DateCreated = standard.DateCreated,
                CreatedBy = standard.CreatedBy,
                DateModified = standard.DateModified,
                DateReceived = standard.DateReceived
            };

            foreach (var invItem in standard.InventoryItems) {
                if (invItem.StockStandard.StockStandardId == standard.StockStandardId) {
                    vStandard.CertificateOfAnalysis = invItem.CertificatesOfAnalysis.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vStandard.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vStandard.UsedFor = invItem.UsedFor;
                    vStandard.Department = invItem.Department;
                    vStandard.CatalogueCode = invItem.CatalogueCode;
                    vStandard.AllCertificatesOfAnalysis = invItem.CertificatesOfAnalysis.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).ToList();
                    vStandard.MSDSNotes = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First().MSDSNotes;
                    vStandard.SupplierName = invItem.SupplierName;
                    vStandard.IsExpired = invItem.StockStandard.ExpiryDate < DateTime.Today;
                    vStandard.IsExpiring = invItem.StockStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(invItem.StockStandard.ExpiryDate < DateTime.Today);
                    vStandard.NumberOfBottles = invItem.NumberOfBottles;
                    vStandard.InitialAmount = invItem.InitialAmount;
                }
            }
            return View(vStandard);
        }

        [Route("Standard/Create")]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor,Quality Assurance")]
        // GET: /Standard/Create
        public ActionResult Create() {
            return View(SetStockStandard(new StockStandardCreateViewModel()));
        }

        // POST: /Standard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Standard/Create")]
        [ValidateAntiForgeryToken]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor,Quality Assurance")]
        public ActionResult Create([Bind(Include = "StockStandardName,SupplierName,CatalogueCode,StorageRequirements,MSDSNotes,LotNumber,MSDSNotes,UsedFor,SolventUsed,Purity,ExpiryDate,NumberOfBottles,InitialAmount,Concentration,DateReceived,IsExpiryDateBasedOnDays,DaysUntilExpired,OtherUnitExplained,ConcentrationOtherUnitExplained")]
                    StockStandardCreateViewModel model, string[] AmountUnit, string[] ConcentrationUnit, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS, string submit) {
            //model isn't valid, return to the form
            if (!ModelState.IsValid) {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(SetStockStandard(model));
            }

            var inventoryRepository = _uow.InventoryItemRepository;

            //catalogue code must be unique - let's verify
            bool doesCatalogueCodeExist = inventoryRepository.Get()
                .Any(item => item.CatalogueCode != null && item.CatalogueCode.Equals(model.CatalogueCode));

            if (doesCatalogueCodeExist) {
                ModelState.AddModelError("", "The Catalogue Code provided is not unique. If the Catalogue Code provided is in fact correct, add the item as a new Lot Number under the existing Catalogue Code.");
                return View(SetStockStandard(model));
            }

            var devicesUsed = Request.Form["Devices"];
            var user = _uow.GetCurrentUser();
            var department = _uow.GetUserDepartment();
            var numOfItems = inventoryRepository.Get().Count();

            if (devicesUsed == null) {
                ModelState.AddModelError("", "You must select a device that was used.");
                return View(SetStockStandard(model));
            }

            model = BuildReagentOrStandard.BuildStandard(model, devicesUsed, AmountUnit, ConcentrationUnit, uploadCofA, uploadMSDS);
            InventoryItem inventoryItem = BuildReagentOrStandard.BuildStandardInventoryItem(model, department);

            StockStandard createStandard = null;
            CheckModelState result = BuildReagentOrStandard.EnterStandardIntoDatabase(model, inventoryItem, numOfItems, department, user.UserName);

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The creation of " + createStandard.StockStandardName + " failed. Please double check all inputs and try again.");
                    return View(SetStockStandard(model));
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    return View(SetStockStandard(model));
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    return View(SetStockStandard(model));
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
                    return View(SetStockStandard(model));
            }
        }

        // GET: /Standard/Edit/5
        [Route("Standard/Edit/{id?}")]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StockStandard stockstandard = _uow.StockStandardRepository.Get(id);

            if (stockstandard == null) {
                return HttpNotFound();
            }

            StockStandardEditViewModel model = new StockStandardEditViewModel() {
                StockStandardId = stockstandard.StockStandardId,
                LotNumber = stockstandard.LotNumber,
                StockStandardName = stockstandard.StockStandardName,
                IdCode = stockstandard.IdCode,
                DateCreated = stockstandard.DateCreated,
                ExpiryDate = stockstandard.ExpiryDate,
                CertificateOfAnalysis = stockstandard.InventoryItems.Where(x => x.StockStandard.StockStandardId == stockstandard.StockStandardId).Select(x => x.CertificatesOfAnalysis.OrderBy(y => y.DateAdded).First()).First(),
                MSDS = stockstandard.InventoryItems.Where(x => x.StockStandard.StockStandardId == stockstandard.StockStandardId).Select(x => x.MSDS.OrderBy(y => y.DateAdded).First()).First()
            };

            foreach (var item in stockstandard.InventoryItems) {
                model.SupplierName = item.SupplierName;
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
                InventoryItemRepository inventoryRepo = _uow.InventoryItemRepository;
                var user = _uow.GetCurrentUser();

                InventoryItem invItem = inventoryRepo.Get()
                        .Where(item => item.StockStandard != null && item.StockStandard.StockStandardId == stockstandard.StockStandardId)
                        .FirstOrDefault();

                StockStandard updateStandard = invItem.StockStandard;
                updateStandard.StockStandardName = stockstandard.StockStandardName;
                updateStandard.IdCode = stockstandard.IdCode;
                updateStandard.LotNumber = stockstandard.LotNumber;
                updateStandard.LastModifiedBy = !string.IsNullOrEmpty(user.UserName) ? user.UserName : "USERID";
                updateStandard.DateModified = DateTime.Today;
                updateStandard.ExpiryDate = stockstandard.ExpiryDate;

                _uow.StockStandardRepository.Update(updateStandard);
                _uow.Commit();

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

                    var msdsRepo = new MSDSRepository();

                    var oldSDS = msdsRepo.Get()
                        .Where(item => item.InventoryItem.StockStandard != null && item.InventoryItem.StockStandard.StockStandardId == stockstandard.StockStandardId)
                        .First();

                    oldSDS.Content = msds.Content;
                    oldSDS.FileName = msds.FileName;
                    oldSDS.ContentType = msds.ContentType;
                    oldSDS.DateAdded = DateTime.Today;

                    msdsRepo.Update(oldSDS);
                    _uow.Commit();
                }

                invItem.SupplierName = stockstandard.SupplierName;
                inventoryRepo.Update(invItem);
                _uow.Commit();

                return RedirectToAction("Details", new { id = stockstandard.StockStandardId });
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
            StockStandard stockstandard = _uow.StockStandardRepository.Get(id);

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
            _uow.StockStandardRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private StockStandardCreateViewModel SetStockStandard(StockStandardCreateViewModel model) {
            var units = _uow.UnitRepository.Get();
            var devices = _uow.DeviceRepository.Get().ToList();
            var userDepartment = _uow.GetUserDepartment();

            model.WeightUnits = units.Where(item => item.UnitType.Equals("Weight")).ToList();
            model.VolumetricUnits = units.Where(item => item.UnitType.Equals("Volume")).ToList();
            model.BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance") && item.Department.Equals(userDepartment) && !item.IsArchived).ToList();
            model.VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric") && item.Department.Equals(userDepartment) && !item.IsArchived).ToList();

            return model;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
