using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
            List<InventoryItem> invRepo = null;

            if (userDepartment.DepartmentName.Equals("Quality Assurance")) {
                invRepo = new InventoryItemRepository().Get().ToList();
            } else {
                invRepo = new InventoryItemRepository().Get().Where(item => item.Department.DepartmentName.Equals(userDepartment.DepartmentName)).ToList();
            }
            
            foreach (var item in invRepo) {
                if (item.StockReagent != null) {
                    lReagents.Add(new StockReagentIndexViewModel() {
                        ReagentId = item.StockReagent.ReagentId,
                        CreatedBy = item.StockReagent.CreatedBy,
                        CatalogueCode = item.CatalogueCode,
                        DateOpened = item.StockReagent.DateOpened,
                        ExpiryDate = item.StockReagent.ExpiryDate,
                        IdCode = item.StockReagent.IdCode,
                        LotNumber = item.StockReagent.LotNumber,
                        ReagentName = item.StockReagent.ReagentName,
                        IsExpired = item.StockReagent.ExpiryDate < DateTime.Today,
                        IsExpiring = item.StockReagent.ExpiryDate < DateTime.Today.AddDays(30) && !(item.StockReagent.ExpiryDate < DateTime.Today)
                    });
                }
            }
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
            var deviceRepo = new DeviceRepository(DbContextSingleton.Instance);

            if (reagent == null) {
                return HttpNotFound();
            }

            var vReagent = new StockReagentDetailsViewModel() {
                ReagentId = reagent.ReagentId,
                LotNumber = reagent.LotNumber,
                IdCode = reagent.IdCode,
                ReagentName = reagent.ReagentName,
                LastModifiedBy = reagent.LastModifiedBy,
                Grade = reagent.Grade,
                GradeAdditionalNotes = reagent.GradeAdditionalNotes,
                DateReceived = reagent.DateReceived,
                DateCreated = reagent.DateCreated,
                CreatedBy = reagent.CreatedBy,
                ExpiryDate = reagent.ExpiryDate,
                DateModified = reagent.DateModified,
                DateOpened = reagent.DateOpened
            };

            foreach (var invItem in reagent.InventoryItems) {
                if (reagent.ReagentId == invItem.StockReagent.ReagentId) {
                    vReagent.CatalogueCode = invItem.CatalogueCode;
                    vReagent.CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.UsedFor = invItem.UsedFor;
                    vReagent.Department = invItem.Department;
                    vReagent.CatalogueCode = invItem.CatalogueCode;
                    vReagent.AllCertificatesOfAnalysis = invItem.CertificatesOfAnalysis.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).ToList();
                    vReagent.MSDSNotes = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First().MSDSNotes;
                    vReagent.IsExpired = invItem.StockReagent.ExpiryDate < DateTime.Today;
                    vReagent.IsExpiring = invItem.StockReagent.ExpiryDate < DateTime.Today.AddDays(30) && !(invItem.StockReagent.ExpiryDate < DateTime.Today);
                    vReagent.SupplierName = invItem.SupplierName;
                    vReagent.NumberOfBottles = invItem.NumberOfBottles;
                    vReagent.InitialAmount = invItem.InitialAmount.Contains("Other") ? invItem.InitialAmount + " (" + invItem.OtherUnitExplained + ")" : invItem.InitialAmount;
                    vReagent.DeviceOne = invItem.FirstDeviceUsed == null ? null : deviceRepo.Get().Where(item => item == invItem.FirstDeviceUsed).First();
                    vReagent.DeviceTwo = invItem.SecondDeviceUsed == null ? null : deviceRepo.Get().Where(item => item == invItem.SecondDeviceUsed).First();
                }
            }
            return View(vReagent);
        }

        // GET: /Reagent/Create
        [Route("Reagent/Create")]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor,Quality Assurance")]
        public ActionResult Create() {
            return View(SetStockReagent(new StockReagentCreateViewModel()));
        }

        // POST: /Reagent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Reagent/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor,Quality Assurance")]
        public ActionResult Create([Bind(Include = "CatalogueCode,MSDSNotes,SupplierName,ReagentName,StorageRequirements,Grade,UsedFor,LotNumber,GradeAdditionalNotes,NumberOfBottles,ExpiryDate,InitialAmount,DateReceived,IsExpiryDateBasedOnDays,DaysUntilExpired,OtherUnitExplained")]
                StockReagentCreateViewModel model, string[] Unit, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS, string submit) {
            //model isn't valid, return to the form
            if (!ModelState.IsValid) {
                return View(SetStockReagent(model));
            }

            var invRepo = new InventoryItemRepository(DbContextSingleton.Instance);

            //catalogue code must be unique - let's verify
            bool doesCatalogueCodeExist = invRepo.Get()
                .Any(item => item.CatalogueCode != null && item.CatalogueCode.Equals(model.CatalogueCode));

            if (doesCatalogueCodeExist) {
                ModelState.AddModelError("", "The Catalogue Code provided is not unique. If the Catalogue Code provided is in fact correct, add the item as a new Lot Number under the existing Catalogue Code.");
                return View(SetStockReagent(model));
            }

            //last line of defense for number of bottles
            if (model.NumberOfBottles == 0) { model.NumberOfBottles = 1; }

            model.InitialAmountUnits = Unit[0];

            if (Unit.Length > 1) {
                model.InitialAmountUnits += "/" + Unit[1];
            }

            var devicesUsed = Request.Form["Devices"];
            var deviceRepo = new DeviceRepository(DbContextSingleton.Instance);


            if (devicesUsed == null) {
                ModelState.AddModelError("", "You must select a device that was used.");
                return View(SetStockReagent(model));
            }

            if (devicesUsed.Contains(",")) {
                model.DeviceOne = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
                model.DeviceTwo = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[1])).FirstOrDefault();
            } else {
                model.DeviceOne = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
            }

            var user = HelperMethods.GetCurrentUser();
            var numOfItems = invRepo.Get().Count();

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
                Department = user.Department,
                UsedFor = model.UsedFor,
                Type = "Reagent",
                StorageRequirements = model.StorageRequirements,
                SupplierName = model.SupplierName,
                NumberOfBottles = model.NumberOfBottles,
                InitialAmount = model.InitialAmount.ToString() + " " + model.InitialAmountUnits,
                OtherUnitExplained = model.OtherUnitExplained,
                FirstDeviceUsed = model.DeviceOne,
                SecondDeviceUsed = model.DeviceTwo
            };

            inventoryItem.MSDS.Add(model.MSDS);
            inventoryItem.CertificatesOfAnalysis.Add(model.CertificateOfAnalysis);
            //getting the enum result

            CheckModelState result = CheckModelState.Invalid;//default to invalid to expect the worst
            StockReagent reagent = null;

            reagent = new StockReagent() {
                LotNumber = model.LotNumber,
                IdCode = user.Department.Location.LocationCode + "-" + (numOfItems + 1) + "-" + model.LotNumber + "/" + model.NumberOfBottles,//append number of bottles
                ReagentName = model.ReagentName,
                Grade = model.Grade,
                GradeAdditionalNotes = model.GradeAdditionalNotes,
                DateReceived = model.DateReceived,
                DateOpened = null,
                DaysUntilExpired = model.DaysUntilExpired,
                ExpiryDate = model.ExpiryDate,
                DateCreated = DateTime.Today,
                CreatedBy = user.UserName,
                DateModified = null,
                CatalogueCode = model.CatalogueCode
            };

            reagent.InventoryItems.Add(inventoryItem);
            result = repo.Create(reagent);

            //if (model.NumberOfBottles > 1) {
            //    for (int i = 1; i <= model.NumberOfBottles; i++) {
            //        reagent = new StockReagent() {
            //            LotNumber = model.LotNumber,
            //            IdCode = department.Location.LocationCode + "-" + (numOfItems + 1) + "-" + model.LotNumber + "/" + i,//append number of bottles
            //            ReagentName = model.ReagentName,
            //            Grade = model.Grade,
            //            GradeAdditionalNotes = model.GradeAdditionalNotes,
            //            DateReceived = model.DateReceived,
            //            DateOpened = null,
            //            DaysUntilExpired = model.DaysUntilExpired,
            //            ExpiryDate = model.ExpiryDate,
            //            DateCreated = DateTime.Today,
            //            CreatedBy = user.UserName,
            //            DateModified = null,
            //            CatalogueCode = model.CatalogueCode
            //        };

            //        reagent.InventoryItems.Add(inventoryItem);
            //        result = repo.Create(reagent);

            //        //creation wasn't successful - break from loop and let switch statement handle the problem
            //        if (result != CheckModelState.Valid) { break; }
            //    }
            //} else {
            //    reagent = new StockReagent() {
            //        LotNumber = model.LotNumber,
            //        IdCode = department.Location.LocationCode + "-" + (numOfItems + 1) + "-" + model.LotNumber,//only 1 bottle, no need to concatenate
            //        ReagentName = model.ReagentName,
            //        Grade = model.Grade,
            //        GradeAdditionalNotes = model.GradeAdditionalNotes,
            //        DateReceived = model.DateReceived,
            //        DateOpened = null,
            //        DaysUntilExpired = model.DaysUntilExpired,
            //        ExpiryDate = model.ExpiryDate,
            //        DateCreated = DateTime.Today,
            //        CreatedBy = user.UserName,
            //        DateModified = null,
            //        CatalogueCode = model.CatalogueCode
            //    };

            //    reagent.InventoryItems.Add(inventoryItem);
            //    result = repo.Create(reagent);
            //}

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The creation of " + reagent.ReagentName + " failed. Please double check all inputs and try again.");
                    return View(SetStockReagent(model));
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    return View(SetStockReagent(model));
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    return View(SetStockReagent(model));
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
                    return View(SetStockReagent(model));
            }
        }

        // GET: /Reagent/Edit/5
        [Route("Reagent/Topup/{id?}")]
        [AuthorizeRedirect(Roles = "Department Head,Administrator,Manager,Supervisor")]
        public ActionResult Topup(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            StockReagent stockreagent = repo.Get(id);

            if (stockreagent == null) {
                return HttpNotFound();
            }
            
            var devices = new DeviceRepository(DbContextSingleton.Instance).Get().ToList();
            var userDepartment = HelperMethods.GetUserDepartment();

            var vReagent = new StockReagentTopUpViewModel() {
                ReagentId = stockreagent.ReagentId,
                LotNumber = stockreagent.LotNumber,
                IdCode = stockreagent.IdCode,
                ReagentName = stockreagent.ReagentName,
                LastModifiedBy = stockreagent.LastModifiedBy,
                Grade = stockreagent.Grade,
                GradeAdditionalNotes = stockreagent.GradeAdditionalNotes,
                DateReceived = stockreagent.DateReceived,
                DateCreated = stockreagent.DateCreated,
                CreatedBy = stockreagent.CreatedBy,
                ExpiryDate = stockreagent.ExpiryDate,
                DateModified = stockreagent.DateModified,
                BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance") && item.Department == userDepartment && !item.IsArchived).ToList(),
                VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric") && item.Department == userDepartment && !item.IsArchived).ToList()
            };

            foreach (var invItem in stockreagent.InventoryItems) {
                if (stockreagent.ReagentId == invItem.StockReagent.ReagentId) {
                    vReagent.CatalogueCode = invItem.CatalogueCode;
                    vReagent.CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.UsedFor = invItem.UsedFor;
                    vReagent.Department = invItem.Department;
                    vReagent.CatalogueCode = invItem.CatalogueCode;
                    vReagent.AllCertificatesOfAnalysis = invItem.CertificatesOfAnalysis.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).ToList();
                    vReagent.MSDSNotes = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First().MSDSNotes;
                    vReagent.SupplierName = invItem.SupplierName;
                    vReagent.NumberOfBottles = invItem.NumberOfBottles;
                    vReagent.InitialAmount = invItem.InitialAmount == null ? invItem.InitialAmount = "N/A" : invItem.InitialAmount.Contains("Other") ? invItem.InitialAmount + " (" + invItem.OtherUnitExplained + ")" : invItem.InitialAmount;
                }
            }
            return View(vReagent);
        }

        [Route("Reagent/Topup/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRedirect(Roles = "Department Head,Administrator,Manager,Supervisor")]
        public ActionResult Topup([Bind(Include = "ReagentId,NewMSDSNotes,NewLotNumber,NewExpiryDate,NewDateReceived,IsExpiryDateBasedOnDays,DaysUntilExpired,CatalogueCode")]
                StockReagentTopUpViewModel model, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS) {
            var reagent = repo.Get(model.ReagentId);

            if (reagent == null) {
                return HttpNotFound();
            }

            if (!ModelState.IsValid) {
                //handle error
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(SetTopupReagent(model, reagent));
            }

            var user = HelperMethods.GetCurrentUser();
            var invRepo = new InventoryItemRepository(DbContextSingleton.Instance);
            var department = HelperMethods.GetUserDepartment();
            var numOfItems = invRepo.Get().Count();

            model.NumberOfBottles = reagent.InventoryItems.Where(item => item.CatalogueCode.Equals(reagent.CatalogueCode)).First().NumberOfBottles;

            //upload CofA and MSDS
            if (uploadCofA != null) {
                var cofa = new CertificateOfAnalysis() {
                    FileName = uploadCofA.FileName,
                    ContentType = uploadCofA.ContentType,
                    DateAdded = DateTime.Today
                };
                using (var reader = new System.IO.BinaryReader(uploadCofA.InputStream)) {
                    cofa.Content = reader.ReadBytes(uploadCofA.ContentLength);
                }
                //model.CertificateOfAnalysis = cofa;
                reagent.InventoryItems.Where(item => item.CatalogueCode.Equals(reagent.CatalogueCode)).First().CertificatesOfAnalysis.Add(cofa);
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
                //model.MSDS = msds;
                //reagent.InventoryItems.Where(item => item.CatalogueCode.Equals(model.CatalogueCode)).First().MSDS.Add(msds);

                var msdsRepo = new MSDSRepository(DbContextSingleton.Instance);

                var oldSDS = msdsRepo.Get()
                    .Where(item => item.InventoryItem.StockReagent != null && item.InventoryItem.StockReagent.CatalogueCode == reagent.CatalogueCode)
                    .First();

                oldSDS.Content = msds.Content;
                oldSDS.FileName = msds.FileName;
                oldSDS.ContentType = msds.ContentType;
                oldSDS.DateAdded = DateTime.Today;

                msdsRepo.Update(oldSDS);
            }

            //write record(s) to the db
            CheckModelState result = CheckModelState.Invalid;//default to invalid to expect the worst
            //set new propeties to create new entity based on old
            StockReagent newReagent = null;
            

            if (model.NumberOfBottles > 1) {
                for (int i = 1; i <= model.NumberOfBottles; i++) {
                    newReagent = new StockReagent() {
                        ExpiryDate = model.NewExpiryDate,
                        IdCode = department.Location.LocationCode + "-" + (numOfItems + 1) + "-" + model.NewLotNumber + "/" + i,//append number of bottles
                        LotNumber = model.NewLotNumber,
                        DateCreated = DateTime.Today,
                        CreatedBy = user.UserName,
                        CatalogueCode = reagent.CatalogueCode,
                        Grade = reagent.Grade,
                        GradeAdditionalNotes = reagent.GradeAdditionalNotes,
                        DateModified = null,
                        DaysUntilExpired = model.DaysUntilExpired,
                        LastModifiedBy = null,
                        ReagentName = reagent.ReagentName,
                        //InventoryItems = reagent.InventoryItems,
                        DateOpened = null,
                        DateReceived = model.NewDateReceived
                    };
                    //reagent.InventoryItems.Add(reagent.InventoryItems.Where(x => x.CatalogueCode.Equals(model.CatalogueCode)).First());
                    result = repo.Create(newReagent);

                    //creation wasn't successful - break from loop and let switch statement handle the problem
                    if (result != CheckModelState.Valid) { break; }
                }
            } else {
                newReagent = new StockReagent() {
                    ExpiryDate = model.NewExpiryDate,
                    IdCode = department.Location.LocationCode + "-" + (numOfItems + 1) + "-" + model.NewLotNumber,//only 1 bottle, no need to concatenate
                    LotNumber = model.NewLotNumber,
                    DateCreated = DateTime.Today,
                    CreatedBy = user.UserName,
                    CatalogueCode = reagent.CatalogueCode,
                    Grade = reagent.Grade,
                    GradeAdditionalNotes = reagent.GradeAdditionalNotes,
                    DateModified = null,
                    DaysUntilExpired = model.DaysUntilExpired,
                    LastModifiedBy = null,
                    ReagentName = reagent.ReagentName,
                    InventoryItems = reagent.InventoryItems,
                    DateOpened = null,
                    DateReceived = model.NewDateReceived
                };

                //reagent.InventoryItems.Add(inventoryItem);
                result = repo.Create(newReagent);
            }

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The creation of " + reagent.ReagentName + " failed. Please double check all inputs and try again.");
                    return View(SetTopupReagent(model, reagent));
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    return View(SetTopupReagent(model, reagent));
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    return View(SetTopupReagent(model, reagent));
                case CheckModelState.Valid:
                    //save pressed
                    return RedirectToAction("Index");
                default:
                    ModelState.AddModelError("", "An unknown error occurred.");
                    return View(SetTopupReagent(model, reagent));
            }
        }

        // GET: /Reagent/Edit/5
        [Route("Reagent/Edit/{id?}")]
        [AuthorizeRedirect(Roles = "Department Head,Administrator,Manager,Supervisor")]
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
                Grade = stockreagent.Grade,
                GradeAdditionalNotes = stockreagent.GradeAdditionalNotes,
                LotNumber = stockreagent.LotNumber,
                ReagentName = stockreagent.ReagentName,
                IdCode = stockreagent.IdCode,
                ExpiryDate = stockreagent.ExpiryDate,
                CertificateOfAnalysis = stockreagent.InventoryItems.Where(x => x.StockReagent.ReagentId == stockreagent.ReagentId).Select(x => x.CertificatesOfAnalysis.OrderBy(y => y.DateAdded).First()).First(),
                MSDS = stockreagent.InventoryItems.Where(x => x.StockReagent.ReagentId == stockreagent.ReagentId).Select(x => x.MSDS.OrderBy(y => y.DateAdded).First()).First()
            };

            foreach (var item in stockreagent.InventoryItems) {
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
        [AuthorizeRedirect(Roles = "Department Head,Administrator,Manager,Supervisor")]
        public ActionResult Edit([Bind(Include = "ReagentId,LotNumber,ExpiryDate,SupplierName,ReagentName,IdCode,Grade,GradeAdditionalNotes")] StockReagentEditViewModel stockreagent, HttpPostedFileBase uploadCofA, HttpPostedFileBase uploadMSDS) {
            var user = HelperMethods.GetCurrentUser();
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid) {
                var invRepo = new InventoryItemRepository(DbContextSingleton.Instance);

                InventoryItem invItem = invRepo.Get()
                        .Where(item => item.StockReagent != null && item.StockReagent.ReagentId == stockreagent.ReagentId)
                        .FirstOrDefault();

                StockReagent updateReagent = invItem.StockReagent;
                updateReagent.LotNumber = stockreagent.LotNumber;
                updateReagent.LastModifiedBy = !string.IsNullOrEmpty(user.UserName) ? user.UserName : "USERID";
                updateReagent.Grade = stockreagent.Grade;
                updateReagent.GradeAdditionalNotes = stockreagent.GradeAdditionalNotes;
                updateReagent.ExpiryDate = stockreagent.ExpiryDate;
                updateReagent.DateModified = DateTime.Today;

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

                    var msdsRepo = new MSDSRepository();

                    var oldSDS = msdsRepo.Get()
                        .Where(item => item.InventoryItem.StockReagent != null && item.InventoryItem.StockReagent.ReagentId == stockreagent.ReagentId)
                        .First();

                    oldSDS.Content = msds.Content;
                    oldSDS.FileName = msds.FileName;
                    oldSDS.ContentType = msds.ContentType;
                    oldSDS.DateAdded = DateTime.Today;

                    msdsRepo.Update(oldSDS);
                }

                invItem.SupplierName = stockreagent.SupplierName;
                invRepo.Update(invItem);

                return RedirectToAction("Details", new { id = stockreagent.ReagentId });
            }
            return View(stockreagent);
        }

        // GET: /Reagent/Delete/5
        [Route("Reagent/Delete/{id?}")]
        [AuthorizeRedirect(Roles = "Department Head,Administrator,Manager,Supervisor")]
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
        [AuthorizeRedirect(Roles = "Department Head,Analyst,Administrator,Manager,Supervisor")]
        public ActionResult DeleteConfirmed(int id) {
            repo.Delete(id);
            return RedirectToAction("Index");
        }

        private StockReagentCreateViewModel SetStockReagent(StockReagentCreateViewModel model) {
            var units = new UnitRepository(DbContextSingleton.Instance).Get();
            var devices = new DeviceRepository(DbContextSingleton.Instance).Get().ToList();
            var userDepartment = HelperMethods.GetUserDepartment();

            model.WeightUnits = units.Where(item => item.UnitType.Equals("Weight")).ToList();
            model.VolumetricUnits = units.Where(item => item.UnitType.Equals("Volume")).ToList();
            model.BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance") && item.Department == userDepartment && !item.IsArchived).ToList();
            model.VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric") && item.Department == userDepartment && !item.IsArchived).ToList();

            return model;
        }

        private StockReagentTopUpViewModel SetTopupReagent(StockReagentTopUpViewModel model, StockReagent stockreagent) {
            var devices = new DeviceRepository(DbContextSingleton.Instance).Get().ToList();
            var userDepartment = HelperMethods.GetUserDepartment();

            model.BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance") && item.Department == userDepartment && !item.IsArchived).ToList();
            model.VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric") && item.Department == userDepartment && !item.IsArchived).ToList();

            var vReagent = new StockReagentTopUpViewModel() {
                ReagentId = stockreagent.ReagentId,
                LotNumber = stockreagent.LotNumber,
                IdCode = stockreagent.IdCode,
                ReagentName = stockreagent.ReagentName,
                LastModifiedBy = stockreagent.LastModifiedBy,
                Grade = stockreagent.Grade,
                GradeAdditionalNotes = stockreagent.GradeAdditionalNotes,
                DateReceived = stockreagent.DateReceived,
                DateCreated = stockreagent.DateCreated,
                CreatedBy = stockreagent.CreatedBy,
                ExpiryDate = stockreagent.ExpiryDate,
                DateModified = stockreagent.DateModified,
                BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance") && item.Department == userDepartment && !item.IsArchived).ToList(),
                VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric") && item.Department == userDepartment && !item.IsArchived).ToList()
            };

            foreach (var invItem in stockreagent.InventoryItems) {
                if (stockreagent.ReagentId == invItem.StockReagent.ReagentId) {
                    vReagent.CatalogueCode = invItem.CatalogueCode;
                    vReagent.CertificateOfAnalysis = invItem.CertificatesOfAnalysis.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vReagent.UsedFor = invItem.UsedFor;
                    vReagent.Department = invItem.Department;
                    vReagent.CatalogueCode = invItem.CatalogueCode;
                    vReagent.AllCertificatesOfAnalysis = invItem.CertificatesOfAnalysis.OrderByDescending(x => x.DateAdded).Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).ToList();
                    vReagent.MSDSNotes = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First().MSDSNotes;
                    vReagent.SupplierName = invItem.SupplierName;
                    vReagent.NumberOfBottles = invItem.NumberOfBottles;
                    vReagent.InitialAmount = invItem.InitialAmount == null ? invItem.InitialAmount = "N/A" : invItem.InitialAmount.Contains("Other") ? invItem.InitialAmount + " (" + invItem.OtherUnitExplained + ")" : invItem.InitialAmount;
                }
            }

            return model;
        }
    }
}
