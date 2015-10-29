using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Filters;
using TMNT.Helpers;
using TMNT.Models;
using TMNT.Models.Enums;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class IntermediateStandardController : Controller {
        private IRepository<IntermediateStandard> repo;

        public IntermediateStandardController() : this(new IntermediateStandardRepository(DbContextSingleton.Instance)) {

        }

        public IntermediateStandardController(IRepository<IntermediateStandard> repo) {
            this.repo = repo;
        }

        // GET: /IntermediateStandard/
        [Route("IntermediateStandard")]
        public ActionResult Index() {
            var userDepartment = HelperMethods.GetUserDepartment();
            List<IntermediateStandardIndexViewModel> lIntStandards = new List<IntermediateStandardIndexViewModel>();

            var invRepo = new InventoryItemRepository().Get()
                .Where(item => item.Type.Equals("Intermediate Standard"))
                .ToList();
            
            foreach (var item in invRepo) {
                if (item.IntermediateStandard != null && item.Department == userDepartment) {
                    lIntStandards.Add(new IntermediateStandardIndexViewModel() {
                        IntermediateStandardId = item.IntermediateStandard.IntermediateStandardId,
                        CreatedBy = item.CreatedBy,
                        DateCreated = item.DateCreated,
                        ExpiryDate = item.ExpiryDate,
                        IdCode = item.IntermediateStandard.IdCode,
                        MaxxamId = item.IntermediateStandard.MaxxamId,
                        IsExpired = item.ExpiryDate < DateTime.Today,
                        IsExpiring = item.ExpiryDate < DateTime.Today.AddDays(30) && !(item.ExpiryDate < DateTime.Today)
                    });
                }
            }

            //old Intermediate Standard Index code. DO NOT DELETE FOR REFERENCE.

            //var intermediatestandards = repo.Get();
            //var list = new List<IntermediateStandardIndexViewModel>();

            //foreach (var item in intermediatestandards) {
            //    list.Add(new IntermediateStandardIndexViewModel() {
            //        IntermediateStandardId = item.IntermediateStandardId,
            //        IdCode = item.IdCode,
            //        MaxxamId = item.MaxxamId
            //    });
            //}
            ////iterating through the associated InventoryItem and retrieving the appropriate data
            ////this is faster than LINQ
            //int counter = 0;
            //foreach (var standard in intermediatestandards) {
            //    foreach (var invItem in standard.InventoryItems) {
            //        if (standard.IntermediateStandardId == invItem.IntermediateStandard.IntermediateStandardId) {
            //            list[counter].ExpiryDate = invItem.ExpiryDate;
            //            list[counter].DateCreated = invItem.DateCreated;
            //            list[counter].CreatedBy = invItem.CreatedBy;
            //        }
            //    }
            //    counter++;
            //}
            return View(lIntStandards);
        }

        // GET: /IntermediateStandard/Details/5
        [Route("IntermediateStandard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IntermediateStandard intermediatestandard = repo.Get(id);
            if (intermediatestandard == null) {
                return HttpNotFound();
            }

            if (Request.UrlReferrer.AbsolutePath.Contains("IntermediateStandard/Details")) {
                ViewBag.ReturnUrl = Request.UrlReferrer.AbsolutePath;
            }

            var vIntermediateStandard = new IntermediateStandardDetailsViewModel() {
                IntermediateStandardId = intermediatestandard.IntermediateStandardId,
                Replaces = intermediatestandard.Replaces,
                ReplacedBy = intermediatestandard.ReplacedBy,
                PrepList = intermediatestandard.PrepList,
                PrepListItems = intermediatestandard.PrepList.PrepListItems.ToList(),
                IdCode = intermediatestandard.IdCode,
                MaxxamId = intermediatestandard.MaxxamId,
                LastModifiedBy = intermediatestandard.LastModifiedBy,
                Concentration = intermediatestandard.FinalConcentration
            };

            foreach (var invItem in intermediatestandard.InventoryItems) {
                if (invItem.IntermediateStandard.IntermediateStandardId == intermediatestandard.IntermediateStandardId) {
                    vIntermediateStandard.ExpiryDate = invItem.ExpiryDate;
                    vIntermediateStandard.DateOpened = invItem.DateOpened;
                    vIntermediateStandard.DateCreated = invItem.DateCreated;
                    vIntermediateStandard.CreatedBy = invItem.CreatedBy;
                    vIntermediateStandard.DateModified = invItem.DateModified;
                    vIntermediateStandard.Department = invItem.Department;
                    //vIntermediateStandard.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vIntermediateStandard.UsedFor = invItem.UsedFor;
                    vIntermediateStandard.IsExpired = invItem.ExpiryDate < DateTime.Today;
                    vIntermediateStandard.IsExpiring = invItem.ExpiryDate < DateTime.Today.AddDays(30) && !(invItem.ExpiryDate < DateTime.Today);
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
        public ActionResult Create([Bind(Include = "IntermediateStandardId,TotalVolume,UsedFor,MaxxamId,FinalConcentration,TotalAmount,ExpiryDate,SafetyNotes")] IntermediateStandardCreateViewModel model,
           string[] PrepListItemTypes, string[] PrepListAmountTakenUnits, string[] PrepListItemAmounts, string[] PrepListItemLotNumbers, string[] TotalAmountUnits, string[] FinalConcentrationUnits, string submit) {

            if (!ModelState.IsValid) {
                var errors = ModelState.Where(item => item.Value.Errors.Any());
                SetIntermediateStandard(model);
                return View(model);
            }

            if (PrepListItemTypes == null || PrepListItemAmounts == null ||  PrepListItemLotNumbers == null) {
                ModelState.AddModelError("", "The creation of " + model.MaxxamId + " failed. Make sure the Prep List table is complete.");
                SetIntermediateStandard(model);
                return View(model);
            }
            //if all 3 arrays are not of equal length, return to view with an error message
            if (!(PrepListItemAmounts.Length == PrepListItemLotNumbers.Length) || !(PrepListItemLotNumbers.Length == PrepListItemTypes.Length)) {
                ModelState.AddModelError("", "The creation of " + model.MaxxamId + " failed. Make sure the Prep List table is complete.");
                SetIntermediateStandard(model);
                return View(model);
            }
            
            //setting the devices used
            var devicesUsed = Request.Form["Devices"];
            var deviceRepo = new DeviceRepository();

            if (devicesUsed == null) {
                ModelState.AddModelError("", "You must select a device that was used.");
                SetIntermediateStandard(model);
                return View(model);
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
            //finished setting teh units to amount and concentration

            var user = HelperMethods.GetCurrentUser();
            var department = HelperMethods.GetUserDepartment();
            var inventoryItemRepo = new InventoryItemRepository();

            InventoryItemRepository invRepo = new InventoryItemRepository(DbContextSingleton.Instance);
            //retrieving all table rows from recipe builder - replace with view model in the future
            IntermediateStandardPrepListItemsViewModel prepListViewModel = new IntermediateStandardPrepListItemsViewModel() {
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
                        var add = new StockReagentRepository(DbContextSingleton.Instance)
                            .Get()
                            .Where(x => x.LotNumber.Equals(lotNumber))
                            .FirstOrDefault();
                        if (add != null) { reagentAndStandardContainer.Add(add); break; }
                    } else if (type.Equals("Standard")) {
                        var add = new StockStandardRepository(DbContextSingleton.Instance)
                            .Get()
                            .Where(x => x.LotNumber.Equals(lotNumber))
                            .FirstOrDefault();
                        if (add != null) { reagentAndStandardContainer.Add(add); break; }
                    } else if (type.Equals("Intermediate Standard")) {
                        var add = new IntermediateStandardRepository(DbContextSingleton.Instance)
                            .Get()
                            .Where(x => x.MaxxamId.Equals(lotNumber))
                            .FirstOrDefault();
                        if (add != null) { reagentAndStandardContainer.Add(add); break; }
                    }
                }
            }

            //loop through all items used and list all items as "opened" if they're not already open
            //if the expiry date hasn't been set yet, set it with the "days until expired" property provided from the "Create" form
            //foreach (var item in reagentAndStandardContainer) {
            //    var invItem = item as InventoryItem;

            //    if (invItem.DateOpened == null) {
            //        invItem.DateOpened = DateTime.Today;
            //    }

            //    if (invItem.ExpiryDate == null) {
            //        invItem.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.DaysUntilExpired));
            //    }
            //    inventoryItemRepo.Update(invItem);
            //}

            //building the prep list with the desired prep list items
            UnitRepository unitRepo = new UnitRepository(DbContextSingleton.Instance);
            int counter = 0;
            foreach (var item in reagentAndStandardContainer) {
                if (item is StockReagent) {
                    var reagent = item as StockReagent;
                    var invItem = reagent.InventoryItems.First();

                    if (invItem.DateOpened == null) {
                        invItem.DateOpened = DateTime.Today;
                    }

                    if (invItem.ExpiryDate == null) {
                        invItem.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.DaysUntilExpired));
                    }
                    inventoryItemRepo.Update(invItem);

                    prepItems.Add(new PrepListItem() {
                        StockReagent = item as StockReagent,
                        AmountTaken = prepListViewModel.AmountsWithUnits[counter]
                    });
                } else if (item is StockStandard) {
                    var standard = item as StockStandard;
                    var invItem = standard.InventoryItems.First();

                    if (invItem.DateOpened == null) {
                        invItem.DateOpened = DateTime.Today;
                    }

                    if (invItem.ExpiryDate == null) {
                        invItem.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.DaysUntilExpired));
                    }
                    inventoryItemRepo.Update(invItem);

                    prepItems.Add(new PrepListItem() {
                        StockStandard = item as StockStandard,
                        AmountTaken = prepListViewModel.AmountsWithUnits[counter]
                    });
                } else if (item is IntermediateStandard) {
                    var intStandard = item as IntermediateStandard;
                    var invItem = intStandard.InventoryItems.First();

                    if (invItem.DateOpened == null) {
                        invItem.DateOpened = DateTime.Today;
                    }

                    if (invItem.ExpiryDate == null) {
                        invItem.ExpiryDate = DateTime.Today.AddDays(Convert.ToInt32(invItem.DaysUntilExpired));
                    }
                    inventoryItemRepo.Update(invItem);

                    prepItems.Add(new PrepListItem() {
                        IntermediateStandard = item as IntermediateStandard,
                        AmountTaken = prepListViewModel.AmountsWithUnits[counter]
                    });
                }
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
                MaxxamId = model.MaxxamId,
                IdCode = department.Location.LocationCode + "-" + (invRepo.Get().Count() + 1) + "-" + model.MaxxamId,// + "/",//append number of bottles?
                PrepList = model.PrepList,
                SafetyNotes = model.SafetyNotes,
                Replaces = !string.IsNullOrEmpty(model.Replaces) ? model.Replaces : "N/A",
                ReplacedBy = !string.IsNullOrEmpty(model.ReplacedBy) ? model.ReplacedBy : "N/A"
            };

            InventoryItem inventoryItem = new InventoryItem() {
                CreatedBy = user.UserName,
                DateReceived = DateTime.Today,//giving this a default value, otherwise errors - never to be looked at
                DateCreated = DateTime.Today,
                Department = department,
                IntermediateStandard = intermediatestandard,
                Type = "Intermediate Standard",
                StorageRequirements = model.StorageRequirements,
                UsedFor = model.UsedFor,
                ExpiryDate = model.ExpiryDate,
                FirstDeviceUsed = model.DeviceOne,
                SecondDeviceUsed = model.DeviceTwo,
                InitialAmount = model.TotalAmount.ToString() + " " + model.TotalAmountUnits
            };

            //creating the prep list and the intermediate standard
            new PrepListRepository(DbContextSingleton.Instance).Create(prepList);
            intermediatestandard.InventoryItems.Add(inventoryItem);
            var result = repo.Create(intermediatestandard);

            switch (result) {
                case CheckModelState.Invalid:
                    ModelState.AddModelError("", "The creation of " +  intermediatestandard.IdCode + " failed. Please double check all inputs and try again.");
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
            IntermediateStandard intermediatestandard = repo.Get(id);
            if (intermediatestandard == null) {
                return HttpNotFound();
            }

            IntermediateStandardEditViewModel model = new IntermediateStandardEditViewModel() {
                IntermediateStandardId = intermediatestandard.IntermediateStandardId,
                Replaces = intermediatestandard.Replaces,
                ReplacedBy = intermediatestandard.ReplacedBy,
                IdCode = intermediatestandard.IdCode,
                MaxxamId = intermediatestandard.MaxxamId
            };

            foreach (var item in intermediatestandard.InventoryItems) {
                if (item.IntermediateStandard.IntermediateStandardId == model.IntermediateStandardId) {
                    model.ExpiryDate = item.ExpiryDate;
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
        public ActionResult Edit([Bind(Include = "IntermediateStandardId,IdCode,MaxxamId,ExpiryDate")] IntermediateStandardEditViewModel intermediatestandard) {
            if (ModelState.IsValid) {
                InventoryItemRepository inventoryRepo = new InventoryItemRepository(DbContextSingleton.Instance);

                InventoryItem invItem = inventoryRepo.Get()
                        .Where(item => item.IntermediateStandard != null && item.IntermediateStandard.IntermediateStandardId == intermediatestandard.IntermediateStandardId)
                        .FirstOrDefault();

                IntermediateStandard updateStandard = invItem.IntermediateStandard;
                updateStandard.IdCode = intermediatestandard.IdCode;
                updateStandard.MaxxamId = intermediatestandard.MaxxamId;
                updateStandard.LastModifiedBy = !string.IsNullOrEmpty(HelperMethods.GetCurrentUser().UserName) ? HelperMethods.GetCurrentUser().UserName : "USERID";

                repo.Update(updateStandard);

                invItem.DateModified = DateTime.Today;
                invItem.ExpiryDate = intermediatestandard.ExpiryDate;


                inventoryRepo.Update(invItem);
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
            IntermediateStandard intermediatestandard = repo.Get(id);
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
            IntermediateStandard intermediatestandard = repo.Get(id);
            repo.Delete(id);
            return RedirectToAction("Index");
        }

        public IntermediateStandardCreateViewModel SetIntermediateStandard(IntermediateStandardCreateViewModel model) {
            var units = new UnitRepository(DbContextSingleton.Instance).Get();
            var devices = new DeviceRepository(DbContextSingleton.Instance).Get().Where(item => item.Department.DepartmentId == HelperMethods.GetUserDepartment().DepartmentId).ToList();
            List<InventoryItem> items = new InventoryItemRepository().Get()
                .Where(item => item.Department == HelperMethods.GetUserDepartment())
                .GroupBy(i => new { i.StockReagent, i.StockStandard, i.IntermediateStandard })
                .Select(g => g.First())
                .ToList();

            model.WeightUnits = units.Where(item => item.UnitType.Equals("Weight")).ToList();
            model.VolumetricUnits = units.Where(item => item.UnitType.Equals("Volume")).ToList();
            model.OtherUnit = units.Where(item => item.UnitType.Equals("Other")).FirstOrDefault();
            model.IntermediateStandards = items.Where(item => item.IntermediateStandard != null).ToList();
            model.StockStandards = items.Where(item => item.StockStandard != null).ToList();
            model.StockReagents = items.Where(item => item.StockReagent != null).ToList();

            model.BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance")).ToList();
            model.VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric")).ToList();

            return model;
        }
    }
}
