using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TMNT.Filters;
using TMNT.Models;
using TMNT.Models.Enums;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class WorkingStandardController : Controller {
        private UnitOfWork _uow;

        public WorkingStandardController()
            : this(new UnitOfWork()) {
        }

        public WorkingStandardController(UnitOfWork uow) {
            this._uow = uow;
        }

        // GET: /WorkingStandard/
        [Route("WorkingStandard")]
        public ActionResult Index() {
            var userDepartment = _uow.GetUserDepartment();
            List<WorkingStandardIndexViewModel> lIntStandards = new List<WorkingStandardIndexViewModel>();

            var invRepo = _uow.InventoryItemRepository.Get()
                .Where(item => item.Type.Equals("Working Standard"))
                .ToList();

            foreach (var item in invRepo) {
                if (item.WorkingStandard != null && item.Department == userDepartment) {
                    lIntStandards.Add(new WorkingStandardIndexViewModel() {
                        WorkingStandardId = item.WorkingStandard.WorkingStandardId,
                        CreatedBy = item.WorkingStandard.CreatedBy,
                        DateCreated = item.WorkingStandard.DateCreated,
                        ExpiryDate = item.WorkingStandard.ExpiryDate,
                        IdCode = item.WorkingStandard.IdCode,
                        MaxxamId = item.WorkingStandard.MaxxamId,
                        IsExpired = item.WorkingStandard.ExpiryDate < DateTime.Today,
                        IsExpiring = item.WorkingStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(item.WorkingStandard.ExpiryDate < DateTime.Today)
                    });
                }
            }
            return View(lIntStandards);
        }

        // GET: /WorkingStandard/Details/5
        [Route("WorkingStandard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var workingStandard = _uow.WorkingStandardRepository.Get(id);
            if (workingStandard == null) {
                return HttpNotFound();
            }

            if (Request.UrlReferrer.AbsolutePath.Contains("WorkingStandard/Details")) {
                ViewBag.ReturnUrl = Request.UrlReferrer.AbsolutePath;
            }

            var vWorkingStandard = new WorkingStandardDetailsViewModel() {
                WorkingStandardId = workingStandard.WorkingStandardId,
                PrepList = workingStandard.PrepList,
                PrepListItems = workingStandard.PrepList.PrepListItems.ToList(),
                IdCode = workingStandard.IdCode,
                MaxxamId = workingStandard.MaxxamId,
                LastModifiedBy = workingStandard.LastModifiedBy,
                Concentration = workingStandard.FinalConcentration,
                ExpiryDate = workingStandard.ExpiryDate,
                DateOpened = workingStandard.DateOpened,
                DateCreated = workingStandard.DateCreated,
                DateModified = workingStandard.DateModified,
                CreatedBy = workingStandard.CreatedBy
            };

            foreach (var invItem in workingStandard.InventoryItems) {
                if (invItem.WorkingStandard.WorkingStandardId == workingStandard.WorkingStandardId) {
                    vWorkingStandard.Department = invItem.Department;
                    vWorkingStandard.UsedFor = invItem.UsedFor;
                    vWorkingStandard.IsExpired = invItem.WorkingStandard.ExpiryDate < DateTime.Today;
                    vWorkingStandard.IsExpiring = invItem.WorkingStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(invItem.WorkingStandard.ExpiryDate < DateTime.Today);
                    vWorkingStandard.InitialAmount = invItem.InitialAmount;
                }
            }
            return View(vWorkingStandard);
        }

        [Route("WorkingStandard/Create")]
        // GET: /WorkingStandard/Create
        public ActionResult Create() {
            return View(SetWorkingStandard(new WorkingStandardCreateViewModel()));
        }

        // POST: /WorkingStandard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("WorkingStandard/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkingStandardId,TotalVolume,UsedFor,FinalConcentration,TotalAmount,ExpiryDate,SafetyNotes,IsExpiryDateBasedOnDays,DaysUntilExpired,OtherUnitExplained,ConcentrationOtherUnitExplained")] WorkingStandardCreateViewModel model,
           string[] PrepListItemTypes, string[] PrepListAmountTakenUnits, string[] PrepListItemAmounts, string[] PrepListItemLotNumbers, string[] TotalAmountUnits, string[] FinalConcentrationUnits, string submit) {

            if (!ModelState.IsValid) {
                var errors = ModelState.Where(item => item.Value.Errors.Any());
                return View(SetWorkingStandard(model));
            }

            if (PrepListItemTypes == null || PrepListItemAmounts == null || PrepListItemLotNumbers == null) {
                ModelState.AddModelError("", "The creation of the Working Standard failed. Make sure the Prep List table is complete.");
                return View(SetWorkingStandard(model));
            }
            //if all 3 arrays are not of equal length, return to view with an error message
            if (!(PrepListItemAmounts.Length == PrepListItemLotNumbers.Length) || !(PrepListItemLotNumbers.Length == PrepListItemTypes.Length)) {
                ModelState.AddModelError("", "The creation of the Working Standard failed. Make sure the Prep List table is complete.");
                return View(SetWorkingStandard(model));
            }

            //setting the devices used
            var devicesUsed = Request.Form["Devices"];
            var deviceRepo = _uow.DeviceRepository;

            if (devicesUsed == null) {
                ModelState.AddModelError("", "You must select a device that was used.");
                return View(SetWorkingStandard(model));
            }

            if (devicesUsed.Contains(",")) {
                model.DeviceOne = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
                model.DeviceTwo = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[1])).FirstOrDefault();
            } else {
                model.DeviceOne = deviceRepo.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
            }
            //finsihed setting the devices used

            //setting the units to amount and concentration
            model.TotalAmountUnits = TotalAmountUnits[0];
            model.FinalConcentrationUnits = FinalConcentrationUnits[0];

            if (TotalAmountUnits.Length > 1) {
                model.TotalAmountUnits += "/" + TotalAmountUnits[1];
            }

            if (FinalConcentrationUnits.Length > 1) {
                model.FinalConcentrationUnits += "/" + FinalConcentrationUnits[1];
            }
            //finished setting the units to amount and concentration

            var user = _uow.GetCurrentUser();
            var invRepo = _uow.InventoryItemRepository;
            var numOfItems = invRepo.Get().Count();

            //retrieving all table rows from recipe builder - replace with view model in the future
            WorkingStandardPrepListItemsViewModel prepListViewModel = new WorkingStandardPrepListItemsViewModel() {
                AmountsWithUnits = PrepListItemAmounts
            };

            prepListViewModel.LotNumbers = PrepListItemLotNumbers;
            prepListViewModel.Types = PrepListItemTypes;

            List<object> reagentAndStandardContainer = new List<object>();
            List<PrepListItem> prepItems = new List<PrepListItem>();

            //go through all types and sort out what they are, instantiate, and build list of objects
            foreach (var lotNumber in prepListViewModel.LotNumbers) {
                foreach (var type in prepListViewModel.Types) {
                    if (type.Equals("Reagent")) {
                        var add = _uow.StockReagentRepository
                            .Get()
                            .Where(x => x.LotNumber.Equals(lotNumber))
                            .FirstOrDefault();
                        if (add != null) { reagentAndStandardContainer.Add(add); break; }
                    } else if (type.Equals("Standard")) {
                        var add = _uow.StockStandardRepository
                            .Get()
                            .Where(x => x.LotNumber.Equals(lotNumber))
                            .FirstOrDefault();
                        if (add != null) { reagentAndStandardContainer.Add(add); break; }
                    } else if (type.Equals("Intermediate Standard")) {
                        var add = _uow.IntermediateStandardRepository
                            .Get()
                            .Where(x => x.IdCode.Equals(lotNumber))
                            .FirstOrDefault();
                        if (add != null) { reagentAndStandardContainer.Add(add); break; }
                    }
                }
            }

            //loop through all items used and list all items as "opened" if they're not already open
            //if the expiry date hasn't been set yet, set it with the "days until expired" property provided from the "Create" form
            //building the prep list with the desired prep list items
            int counter = 0;
            foreach (var item in reagentAndStandardContainer) {
                if (item is StockReagent) {
                    var reagent = item as StockReagent;
                    var invItem = invRepo.Get().Where(x => x.CatalogueCode != null && x.CatalogueCode.Equals(reagent.CatalogueCode)).First();

                    if (invItem.StockReagent.DateOpened == null) {
                        invItem.StockReagent.DateOpened = DateTime.Today;
                    }

                    if (invItem.StockReagent.ExpiryDate == null) {
                        invItem.StockReagent.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.StockReagent.DaysUntilExpired));
                    }

                    invItem.StockReagent.LastModifiedBy = user.UserName;
                    invItem.StockReagent.DateModified = DateTime.Today;

                    invRepo.Update(invItem);

                    prepItems.Add(new PrepListItem() {
                        StockReagent = reagent,
                        AmountTaken = prepListViewModel.AmountsWithUnits[counter]
                    });
                } else if (item is StockStandard) {
                    var standard = item as StockStandard;
                    var invItem = invRepo.Get().Where(x => x.CatalogueCode != null && x.CatalogueCode.Equals(standard.CatalogueCode)).First();

                    if (invItem.StockStandard.DateOpened == null) {
                        invItem.StockStandard.DateOpened = DateTime.Today;
                    }

                    if (invItem.StockStandard.ExpiryDate == null) {
                        invItem.StockStandard.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.StockStandard.DaysUntilExpired));
                    }

                    invItem.StockStandard.LastModifiedBy = user.UserName;
                    invItem.StockStandard.DateModified = DateTime.Today;

                    invRepo.Update(invItem);

                    prepItems.Add(new PrepListItem() {
                        StockStandard = standard,
                        AmountTaken = prepListViewModel.AmountsWithUnits[counter]
                    });
                } else if (item is IntermediateStandard) {
                    var intStandard = item as IntermediateStandard;
                    var invItem = intStandard.InventoryItems.First();

                    if (invItem.IntermediateStandard.DateOpened == null) {
                        invItem.IntermediateStandard.DateOpened = DateTime.Today;
                    }

                    if (invItem.IntermediateStandard.ExpiryDate == null) {
                        invItem.IntermediateStandard.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.IntermediateStandard.DaysUntilExpired));
                    }

                    invItem.IntermediateStandard.LastModifiedBy = user.UserName;
                    invItem.IntermediateStandard.DateModified = DateTime.Today;

                    invRepo.Update(invItem);

                    prepItems.Add(new PrepListItem() {
                        IntermediateStandard = intStandard,
                        AmountTaken = prepListViewModel.AmountsWithUnits[counter]
                    });
                }
                _uow.Commit();
                counter++;
            }

            PrepList prepList = new PrepList() {
                PrepListItems = prepItems
            };

            model.PrepList = prepList;

            //building the Working standard
            WorkingStandard Workingstandard = new WorkingStandard() {
                TotalVolume = model.TotalAmount.ToString() + " " + model.TotalAmountUnits,
                FinalConcentration = model.FinalConcentration.ToString() + " " + model.FinalConcentrationUnits,
                MaxxamId = user.Department.Location.LocationCode + "-" + (numOfItems + 1),// + "-" + model.MaxxamId,//append number of bottles// model.MaxxamId,
                IdCode = user.Department.Location.LocationCode + "-" + (invRepo.Get().Count() + 1),// + "-" + model.MaxxamId,// + "/",//append number of bottles?
                PrepList = model.PrepList,
                SafetyNotes = model.SafetyNotes,
                CreatedBy = user.UserName,
                DateCreated = DateTime.Today,
                ExpiryDate = model.ExpiryDate,
                DaysUntilExpired = model.DaysUntilExpired
            };

            InventoryItem inventoryItem = new InventoryItem() {
                Department = user.Department,
                WorkingStandard = Workingstandard,
                Type = "Working Standard",
                StorageRequirements = model.StorageRequirements,
                UsedFor = model.UsedFor,
                FirstDeviceUsed = model.DeviceOne,
                SecondDeviceUsed = model.DeviceTwo,
                OtherUnitExplained = model.OtherUnitExplained,
                SupplierName = "Maxxam",
                ConcentrationOtherUnitExplained = model.ConcentrationOtherUnitExplained,
                InitialAmount = model.TotalAmount.ToString() + " " + model.TotalAmountUnits
            };

            //creating the prep list and the Working standard
            _uow.PrepListRepository.Create(prepList);
            Workingstandard.InventoryItems.Add(inventoryItem);
            _uow.WorkingStandardRepository.Create(Workingstandard);

            var result = _uow.Commit();

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The creation of " + Workingstandard.IdCode + " failed. Please double check all inputs and try again.");
                    return View(SetWorkingStandard(model));
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    return View(SetWorkingStandard(model));
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    return View(SetWorkingStandard(model));
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
                    return View(SetWorkingStandard(model));
            }
        }

        // GET: /WorkingStandard/Edit/5
        [Route("WorkingStandard/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingStandard Workingstandard = _uow.WorkingStandardRepository.Get(id);
            if (Workingstandard == null) {
                return HttpNotFound();
            }

            WorkingStandardEditViewModel model = new WorkingStandardEditViewModel() {
                WorkingStandardId = Workingstandard.WorkingStandardId,
                IdCode = Workingstandard.IdCode,
                MaxxamId = Workingstandard.MaxxamId
            };

            foreach (var item in Workingstandard.InventoryItems) {
                if (item.WorkingStandard.WorkingStandardId == model.WorkingStandardId) {
                    model.ExpiryDate = item.WorkingStandard.ExpiryDate;
                }
            }
            return View(model);
        }

        // POST: /WorkingStandard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("WorkingStandard/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkingStandardId,IdCode,MaxxamId,ExpiryDate")] WorkingStandardEditViewModel workingStandard) {
            if (ModelState.IsValid) {
                InventoryItemRepository inventoryRepo = _uow.InventoryItemRepository;
                var user = _uow.GetCurrentUser();

                InventoryItem invItem = inventoryRepo.Get()
                        .Where(item => item.WorkingStandard != null && item.WorkingStandard.WorkingStandardId == workingStandard.WorkingStandardId)
                        .FirstOrDefault();

                WorkingStandard updateStandard = invItem.WorkingStandard;
                updateStandard.IdCode = workingStandard.IdCode;
                updateStandard.MaxxamId = workingStandard.MaxxamId;
                updateStandard.LastModifiedBy = user.UserName;
                updateStandard.DateModified = DateTime.Today;
                updateStandard.ExpiryDate = workingStandard.ExpiryDate;

                _uow.WorkingStandardRepository.Update(updateStandard);
                //_uow.Commit();

                return RedirectToAction("Index");
            }
            return View(workingStandard);
        }

        // GET: /WorkingStandard/Delete/5
        [Route("WorkingStandard/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingStandard Workingstandard = _uow.WorkingStandardRepository.Get(id);
            if (Workingstandard == null) {
                return HttpNotFound();
            }
            return View(Workingstandard);
        }

        // POST: /WorkingStandard/Delete/5
        [Route("WorkingStandard/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            WorkingStandard Workingstandard = _uow.WorkingStandardRepository.Get(id);
            _uow.WorkingStandardRepository.Delete(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }

        private WorkingStandardCreateViewModel SetWorkingStandard(WorkingStandardCreateViewModel model) {
            var department = _uow.GetUserDepartment();
            var units = _uow.UnitRepository.Get();
            var devices = _uow.DeviceRepository.Get().Where(item => item.Department.DepartmentId == department.DepartmentId).ToList();

            List<InventoryItem> items = _uow.InventoryItemRepository.Get()
                .Where(item => item.Department == department)
                .GroupBy(i => new { i.StockReagent, i.StockStandard, i.WorkingStandard })
                .Select(g => g.First())
                .ToList();

            model.WeightUnits = units.Where(item => item.UnitType.Equals("Weight")).ToList();
            model.VolumetricUnits = units.Where(item => item.UnitType.Equals("Volume")).ToList();
            model.OtherUnit = units.Where(item => item.UnitType.Equals("Other")).FirstOrDefault();
            model.IntermediateStandards = items.Where(item => item.IntermediateStandard != null && item.Department == department).ToList();
            model.StockStandards = items.Where(item => item.StockStandard != null && item.Department == department).ToList();
            model.StockReagents = items.Where(item => item.StockReagent != null && item.Department == department).ToList();
            model.Department = department;

            model.BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance") && item.Department == department).ToList();
            model.VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric") && item.Department == department).ToList();

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
