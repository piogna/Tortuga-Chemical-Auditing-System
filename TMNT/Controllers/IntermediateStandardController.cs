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
    public class IntermediateStandardController : Controller {
        private UnitOfWork _uow;

        public IntermediateStandardController(UnitOfWork uow) {
            _uow = uow;
        }

        public IntermediateStandardController() : this(new UnitOfWork()) { }

        // GET: /IntermediateStandard/
        [Route("IntermediateStandard")]
        public ActionResult Index() {
            var userDepartment = _uow.GetUserDepartment();
            var lIntStandards = new List<IntermediateStandardIndexViewModel>();

            var invRepo = _uow.InventoryItemRepository.Get()
                .Where(item => item.IntermediateStandard != null)
                .ToList();

            foreach (var item in invRepo) {
                if (item.IntermediateStandard != null && item.Department == userDepartment) {
                    lIntStandards.Add(new IntermediateStandardIndexViewModel() {
                        IntermediateStandardId = item.IntermediateStandard.IntermediateStandardId,
                        CreatedBy = item.IntermediateStandard.CreatedBy,
                        DateCreated = item.IntermediateStandard.DateCreated,
                        ExpiryDate = item.IntermediateStandard.ExpiryDate,
                        IdCode = item.IntermediateStandard.IdCode,
                        DateOpened = item.IntermediateStandard.DateOpened,
                        IsExpired = item.IntermediateStandard.ExpiryDate < DateTime.Today,
                        IsExpiring = item.IntermediateStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(item.IntermediateStandard.ExpiryDate < DateTime.Today)
                    });
                }
            }
            return View(lIntStandards);
        }

        // GET: /IntermediateStandard/Details/5
        [Route("IntermediateStandard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IntermediateStandard intermediatestandard = _uow.IntermediateStandardRepository.Get(id);
            if (intermediatestandard == null) {
                return HttpNotFound();
            }

            if (Session["FirstIStandardViewed"] == null) {
                Session["FirstIStandardViewed"] = intermediatestandard.IntermediateStandardId;
            }

            if (Convert.ToInt32(Session["FirstIStandardViewed"]) != intermediatestandard.IntermediateStandardId) {
                ViewBag.ReturnUrl = Request.UrlReferrer.AbsolutePath;
            }

            var vIntermediateStandard = new IntermediateStandardDetailsViewModel() {
                IntermediateStandardId = intermediatestandard.IntermediateStandardId,
                Replaces = intermediatestandard.Replaces,
                ReplacedBy = intermediatestandard.ReplacedBy,
                PrepList = intermediatestandard.PrepList,
                PrepListItems = intermediatestandard.PrepList.PrepListItems.ToList(),
                IdCode = intermediatestandard.IdCode,
                LastModifiedBy = intermediatestandard.LastModifiedBy,
                Concentration = intermediatestandard.FinalConcentration,
                ExpiryDate = intermediatestandard.ExpiryDate,
                DateModified = intermediatestandard.DateModified,
                DateOpened = intermediatestandard.DateOpened,
                DateCreated = intermediatestandard.DateCreated,
                CreatedBy = intermediatestandard.CreatedBy
            };

            foreach (var invItem in intermediatestandard.InventoryItems) {
                if (invItem.IntermediateStandard.IntermediateStandardId == intermediatestandard.IntermediateStandardId) {
                    vIntermediateStandard.Department = invItem.Department;
                    vIntermediateStandard.UsedFor = invItem.UsedFor;
                    vIntermediateStandard.IsExpired = invItem.IntermediateStandard.ExpiryDate < DateTime.Today;
                    vIntermediateStandard.IsExpiring = invItem.IntermediateStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(invItem.IntermediateStandard.ExpiryDate < DateTime.Today);
                    vIntermediateStandard.InitialAmount = invItem.InitialAmount;
                }
            }
            return View(vIntermediateStandard);
        }

        [Route("IntermediateStandard/Create")]
        // GET: /IntermediateStandard/Create
        public ActionResult Create() {
            var model = new IntermediateStandardCreateViewModel();
            SetIntermediateStandard(model);

            return View(model);
        }

        // POST: /IntermediateStandard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("IntermediateStandard/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IntermediateStandardId,TotalVolume,UsedFor,FinalConcentration,TotalAmount,ExpiryDate,SafetyNotes,IsExpiryDateBasedOnDays,DaysUntilExpired,OtherUnitExplained,ConcentrationOtherUnitExplained")] IntermediateStandardCreateViewModel model,
           string[] PrepListItemTypes, string[] PrepListAmountTakenUnits, string[] PrepListItemAmounts, string[] PrepListItemLotNumbers, string[] TotalAmountUnits, string[] FinalConcentrationUnits, string submit) {

            if (!ModelState.IsValid) {
                var errors = ModelState.Where(item => item.Value.Errors.Any());
                SetIntermediateStandard(model);
                return View(model);
            }

            if (PrepListItemTypes == null || PrepListItemAmounts == null || PrepListItemLotNumbers == null) {
                ModelState.AddModelError("", "The creation of the Intermediate Standard failed. Make sure the Prep List table is complete.");
                SetIntermediateStandard(model);
                return View(model);
            }
            //if all 3 arrays are not of equal length, return to view with an error message
            if (!(PrepListItemAmounts.Length == PrepListItemLotNumbers.Length) || !(PrepListItemLotNumbers.Length == PrepListItemTypes.Length)) {
                ModelState.AddModelError("", "The creation of the Intermediate Standard failed. Make sure the Prep List table is complete.");
                SetIntermediateStandard(model);
                return View(model);
            }

            //setting the devices used
            var devicesUsed = Request.Form["Devices"];

            if (devicesUsed == null) {
                ModelState.AddModelError("", "You must select a device that was used.");
                SetIntermediateStandard(model);
                return View(model);
            }

            if (devicesUsed.Contains(",")) {
                model.DeviceOne = _uow.DeviceRepository.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
                model.DeviceTwo = _uow.DeviceRepository.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[1])).FirstOrDefault();
            } else {
                model.DeviceOne = _uow.DeviceRepository.Get().Where(item => item.DeviceCode.Equals(devicesUsed.Split(',')[0])).FirstOrDefault();
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
            //finished setting teh units to amount and concentration

            var user = _uow.GetCurrentUser();
            var department = _uow.GetUserDepartment();
            var numOfItems = _uow.InventoryItemRepository.Get().Count();

            //retrieving all table rows from recipe builder - replace with view model in the future
            var prepListViewModel = new IntermediateStandardPrepListItemsViewModel() {
                AmountsWithUnits = PrepListItemAmounts
            };

            prepListViewModel.LotNumbers = PrepListItemLotNumbers;
            prepListViewModel.Types = PrepListItemTypes;

            var reagentAndStandardContainer = new List<object>();
            var prepItems = new List<PrepListItem>();

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
                    var invItem = _uow.InventoryItemRepository.Get().Where(x => x != null && x.CatalogueCode != null && x.CatalogueCode.Equals(reagent.CatalogueCode)).First();

                    if (invItem.StockReagent.DateOpened == null) {
                        invItem.StockReagent.DateOpened = DateTime.Today;
                    }

                    if (invItem.StockReagent.ExpiryDate == null) {
                        invItem.StockReagent.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.StockReagent.DaysUntilExpired));
                    }

                    invItem.StockReagent.LastModifiedBy = user.UserName;
                    invItem.StockReagent.DateModified = DateTime.Today;

                    _uow.InventoryItemRepository.Update(invItem);

                    prepItems.Add(new PrepListItem() {
                        StockReagent = reagent,
                        AmountTaken = prepListViewModel.AmountsWithUnits[counter]
                    });
                } else if (item is StockStandard) {
                    var standard = item as StockStandard;
                    var invItem = _uow.InventoryItemRepository.Get().Where(x => x != null && x.CatalogueCode != null && x.CatalogueCode.Equals(standard.CatalogueCode)).First();

                    if (invItem.StockStandard.DateOpened == null) {
                        invItem.StockStandard.DateOpened = DateTime.Today;
                    }

                    if (invItem.StockStandard.ExpiryDate == null) {
                        invItem.StockStandard.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.StockStandard.DaysUntilExpired));
                    }

                    invItem.StockStandard.LastModifiedBy = user.UserName;
                    invItem.StockStandard.DateModified = DateTime.Today;

                    _uow.InventoryItemRepository.Update(invItem);

                    prepItems.Add(new PrepListItem() {
                        StockStandard = standard,
                        AmountTaken = prepListViewModel.AmountsWithUnits[counter]
                    });
                } else if (item is IntermediateStandard) {
                    var intStandard = item as IntermediateStandard;
                    var invItem = _uow.InventoryItemRepository.Get().Where(x => x.IntermediateStandard == intStandard).First();

                    if (invItem.IntermediateStandard.DateOpened == null) {
                        invItem.IntermediateStandard.DateOpened = DateTime.Today;
                    }

                    if (invItem.IntermediateStandard.ExpiryDate == null) {
                        invItem.IntermediateStandard.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.IntermediateStandard.DaysUntilExpired));
                    }

                    invItem.IntermediateStandard.LastModifiedBy = user.UserName;
                    invItem.IntermediateStandard.DateModified = DateTime.Today;

                    _uow.InventoryItemRepository.Update(invItem);

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

            //building the intermediate standard
            IntermediateStandard intermediatestandard = new IntermediateStandard() {
                TotalVolume = model.TotalAmount.ToString() + " " + model.TotalAmountUnits,
                FinalConcentration = model.FinalConcentration.ToString() + " " + model.FinalConcentrationUnits,
                FinalVolume = model.FinalVolume,
                IdCode = department.Location.LocationCode + "-" + (_uow.InventoryItemRepository.Get().Count() + 1) + "-" + model.CreatedBy,// + "/",//append number of bottles?
                PrepList = model.PrepList,
                SafetyNotes = model.SafetyNotes,
                CreatedBy = user.UserName,
                DateCreated = DateTime.Today,
                ExpiryDate = model.ExpiryDate,
                DaysUntilExpired = model.DaysUntilExpired,
                Replaces = !string.IsNullOrEmpty(model.Replaces) ? model.Replaces : "N/A",
                ReplacedBy = !string.IsNullOrEmpty(model.ReplacedBy) ? model.ReplacedBy : "N/A"
            };

            InventoryItem inventoryItem = new InventoryItem() {
                Department = department,
                IntermediateStandard = intermediatestandard,
                Type = "Intermediate Standard",
                StorageRequirements = model.StorageRequirements,
                UsedFor = model.UsedFor,
                FirstDeviceUsed = model.DeviceOne,
                SecondDeviceUsed = model.DeviceTwo,
                OtherUnitExplained = model.OtherUnitExplained,
                SupplierName = "Maxxam",
                ConcentrationOtherUnitExplained = model.ConcentrationOtherUnitExplained,
                InitialAmount = model.TotalAmount.ToString() + " " + model.TotalAmountUnits,
            };

            //creating the prep list and the intermediate standard
            _uow.PrepListRepository.Create(prepList);
            intermediatestandard.InventoryItems.Add(inventoryItem);
            _uow.IntermediateStandardRepository.Create(intermediatestandard);

            var result = _uow.Commit();

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The creation of " + intermediatestandard.IdCode + " failed. Please double check all inputs and try again.");
                    SetIntermediateStandard(model);
                    return View(model);
                case CheckModelState.DataError:
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists please contact your system administrator.");
                    SetIntermediateStandard(model);
                    return View(model);
                case CheckModelState.Error:
                    ModelState.AddModelError("", "There was an error. Please try again.");
                    SetIntermediateStandard(model);
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
                    SetIntermediateStandard(model);
                    return View(model);
            }
        }

        // GET: /IntermediateStandard/Edit/5
        [Route("IntermediateStandard/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntermediateStandard intermediatestandard = _uow.IntermediateStandardRepository.Get(id);
            if (intermediatestandard == null) {
                return HttpNotFound();
            }

            IntermediateStandardEditViewModel model = new IntermediateStandardEditViewModel() {
                IntermediateStandardId = intermediatestandard.IntermediateStandardId,
                Replaces = intermediatestandard.Replaces,
                ReplacedBy = intermediatestandard.ReplacedBy,
                IdCode = intermediatestandard.IdCode
            };

            foreach (var item in intermediatestandard.InventoryItems) {
                if (item.IntermediateStandard.IntermediateStandardId == model.IntermediateStandardId) {
                    model.ExpiryDate = item.IntermediateStandard.ExpiryDate;
                }
            }
            return View(model);
        }

        // POST: /IntermediateStandard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("IntermediateStandard/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IntermediateStandardId,IdCode,ExpiryDate")] IntermediateStandardEditViewModel intermediatestandard) {
            if (ModelState.IsValid) {
                var user = _uow.GetCurrentUser();

                var invItem = _uow.InventoryItemRepository.Get()
                        .Where(item => item.IntermediateStandard != null && item.IntermediateStandard.IntermediateStandardId == intermediatestandard.IntermediateStandardId)
                        .FirstOrDefault();

                IntermediateStandard updateStandard = invItem.IntermediateStandard;
                updateStandard.IdCode = intermediatestandard.IdCode;
                updateStandard.DateModified = DateTime.Today;
                updateStandard.ExpiryDate = intermediatestandard.ExpiryDate;
                updateStandard.LastModifiedBy = user.UserName;

                _uow.IntermediateStandardRepository.Update(updateStandard);

                _uow.InventoryItemRepository.Update(invItem);
                //_uow.Commit();
                return RedirectToAction("Index");
            }
            return View(intermediatestandard);
        }

        // GET: /IntermediateStandard/Delete/5
        [Route("IntermediateStandard/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntermediateStandard intermediatestandard = _uow.IntermediateStandardRepository.Get(id);
            if (intermediatestandard == null) {
                return HttpNotFound();
            }
            return View(intermediatestandard);
        }

        // POST: /IntermediateStandard/Delete/5
        [Route("IntermediateStandard/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            IntermediateStandard intermediatestandard = _uow.IntermediateStandardRepository.Get(id);
            _uow.IntermediateStandardRepository.Delete(id);
            _uow.Commit();
            return RedirectToAction("Index");
        }

        private IntermediateStandardCreateViewModel SetIntermediateStandard(IntermediateStandardCreateViewModel model) {
            var department = _uow.GetUserDepartment();
            var units = _uow.UnitRepository.Get();
            var devices = _uow.DeviceRepository.Get().Where(item => item.Department.DepartmentId == department.DepartmentId).ToList();

            var items = _uow.InventoryItemRepository.Get()
                .Where(item => item.Department == department)
                .GroupBy(i => new { i.StockReagent, i.StockStandard, i.IntermediateStandard })
                .Select(g => g.First())
                .ToList();

            model.WeightUnits = units.Where(item => item.UnitType.Equals("Weight")).ToList();
            model.VolumetricUnits = units.Where(item => item.UnitType.Equals("Volume")).ToList();
            model.OtherUnit = units.Where(item => item.UnitType.Equals("Other")).FirstOrDefault();
            model.IntermediateStandards = items.Where(item => item.IntermediateStandard != null && item.Department == department).ToList();
            model.StockStandards = items.Where(item => item.StockStandard != null && item.Department == department).ToList();
            model.StockReagents = items.Where(item => item.StockReagent != null && item.Department == department).ToList();
            model.Department = department;

            model.BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance") && item.Department == department && !item.IsArchived).ToList();
            model.VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric") && item.Department == department && !item.IsArchived).ToList();

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
