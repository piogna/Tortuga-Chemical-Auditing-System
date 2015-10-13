using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Helpers;
using TMNT.Models;
using TMNT.Models.Enums;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    [Authorize]
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

            if (Request.UrlReferrer.AbsolutePath.Contains("IntermediateStandard")) {
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
                LastModifiedBy = intermediatestandard.LastModifiedBy
            };

            foreach (var invItem in intermediatestandard.InventoryItems) {
                if (invItem.IntermediateStandard.IntermediateStandardId == intermediatestandard.IntermediateStandardId) {
                    vIntermediateStandard.ExpiryDate = invItem.ExpiryDate;
                    vIntermediateStandard.DateOpened = invItem.DateOpened;
                    vIntermediateStandard.DateCreated = invItem.DateCreated;
                    vIntermediateStandard.CreatedBy = invItem.CreatedBy;
                    vIntermediateStandard.DateModified = invItem.DateModified;
                    vIntermediateStandard.Unit = invItem.Unit;
                    //vIntermediateStandard.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vIntermediateStandard.UsedFor = invItem.UsedFor;
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
        public ActionResult Create([Bind(Include = "IntermediateStandardId,TotalVolume,UsedFor,MaxxamId,FinalConcentration,FinalVolume,TotalAmount,ExpiryDate,IdCode")] IntermediateStandardCreateViewModel model,
           string[] PrepListItemTypes, string[] PrepListItemAmounts, string[] PrepListItemLotNumbers, string submit) {

            if (!ModelState.IsValid) {
                var errors = ModelState.Where(item => item.Value.Errors.Any());
                SetIntermediateStandard(model);
                return View(model);
            }

            if (PrepListItemTypes == null || PrepListItemAmounts == null || PrepListItemLotNumbers == null) {
                ModelState.AddModelError("", "The creation of " + model.IdCode + " failed. Make sure the Prep List table is complete.");
                SetIntermediateStandard(model);
                return View(model);
            }
            //if all 3 arrays are not of equal length, return to view with an error message
            if (!(PrepListItemAmounts.Length == PrepListItemLotNumbers.Length) || !(PrepListItemLotNumbers.Length == PrepListItemTypes.Length)) {
                ModelState.AddModelError("", "The creation of " + model.IdCode + " failed. Make sure the Prep List table is complete.");
                SetIntermediateStandard(model);
                return View(model);
            }

            //if (uploadMSDS != null) {
            //    var msds = new MSDS() {
            //        FileName = uploadMSDS.FileName,
            //        ContentType = uploadMSDS.ContentType,
            //        DateAdded = DateTime.Today,
            //        MSDSNotes = model.MSDSNotes
            //    };
            //    using (var reader = new System.IO.BinaryReader(uploadMSDS.InputStream)) {
            //        msds.Content = reader.ReadBytes(uploadMSDS.ContentLength);
            //    }
            //    model.MSDS = msds;
            //}

            InventoryItemRepository invRepo = new InventoryItemRepository(DbContextSingleton.Instance);
            //retrieving all table rows from recipe builder - replace with view model in the future
            IntermediateStandardPrepListItemsViewModel prepListViewModel = new IntermediateStandardPrepListItemsViewModel() {
                AmountsWithUnits = PrepListItemAmounts//Request.Form.GetValues("Amount").Where(item => !string.IsNullOrEmpty(item)).ToArray()
            };

            prepListViewModel.Amounts = prepListViewModel.AmountsWithUnits.Select(item => item.Split(' ')[0]).ToArray();
            prepListViewModel.Units = prepListViewModel.AmountsWithUnits.Select(item => item.Split(' ')[1]).ToArray();
            prepListViewModel.LotNumbers = PrepListItemLotNumbers;
            prepListViewModel.Types = PrepListItemTypes;

            List<object> reagentAndStandardContainer = new List<object>();
            List<PrepListItem> prepItems = new List<PrepListItem>();

            //go through all types and sort out what they are, instantiate, and build list of objects
            foreach (var lotNumber in prepListViewModel.LotNumbers) {
                foreach (var type in prepListViewModel.Types) {
                    if (type == "Reagent") {
                        var add = new StockReagentRepository(DbContextSingleton.Instance)
                            .Get()
                            .Where(x => x.LotNumber == lotNumber)
                            .FirstOrDefault();
                        if (add != null) { reagentAndStandardContainer.Add(add); break; }
                    } else if (type == "Standard") {
                        var add = new StockStandardRepository(DbContextSingleton.Instance)
                            .Get()
                            .Where(x => x.LotNumber == lotNumber)
                            .FirstOrDefault();
                        if (add != null) { reagentAndStandardContainer.Add(add); break; }
                    } else if (type == "Intermediate Standard") {
                        var add = new IntermediateStandardRepository(DbContextSingleton.Instance)
                            .Get()
                            .Where(x => x.MaxxamId == lotNumber)
                            .FirstOrDefault();
                        if (add != null) { reagentAndStandardContainer.Add(add); break; }
                    }
                }
            }

            //building the prep list with the desired prep list items
            UnitRepository unitRepo = new UnitRepository(DbContextSingleton.Instance);
            int counter = 0;
            foreach (var item in reagentAndStandardContainer) {
                if (item is StockReagent) {
                    prepItems.Add(new PrepListItem() {
                        StockReagent = item as StockReagent,
                        Unit = unitRepo.Get().Where(unit => unit.UnitShorthandName.Equals(prepListViewModel.Units[counter])).First(),
                        Amount = Convert.ToInt32(prepListViewModel.Amounts[counter])
                    });
                } else if (item is StockStandard) {
                    prepItems.Add(new PrepListItem() {
                        StockStandard = item as StockStandard,
                        Unit = unitRepo.Get().Where(unit => unit.UnitShorthandName.Equals(prepListViewModel.Units[counter])).First(),
                        Amount = Convert.ToInt32(prepListViewModel.Amounts[counter])
                    });
                } else if (item is IntermediateStandard) {
                    prepItems.Add(new PrepListItem() {
                        IntermediateStandard = item as IntermediateStandard,
                        Unit = unitRepo.Get().Where(unit => unit.UnitShorthandName.Equals(prepListViewModel.Units[counter])).First(),
                        Amount = Convert.ToInt32(prepListViewModel.Amounts[counter])
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
                TotalVolume = model.TotalAmount,
                FinalConcentration = model.FinalConcentration,
                FinalVolume = model.FinalVolume,
                MaxxamId = model.MaxxamId,
                IdCode = model.IdCode,
                PrepList = model.PrepList,
                Replaces = !string.IsNullOrEmpty(model.Replaces) ? model.Replaces : "N/A",
                ReplacedBy = !string.IsNullOrEmpty(model.ReplacedBy) ? model.ReplacedBy : "N/A"
            };

            InventoryItem inventoryItem = new InventoryItem() {
                CreatedBy = !string.IsNullOrEmpty(HelperMethods.GetCurrentUser().UserName) ? HelperMethods.GetCurrentUser().UserName : "USERID",
                DateCreated = DateTime.Today,
                Department = HelperMethods.GetUserDepartment(),
                IntermediateStandard = intermediatestandard,
                Type = "Intermediate Standard",
                StorageRequirements = model.StorageRequirements,
                UsedFor = model.UsedFor,
                ExpiryDate = model.ExpiryDate
            };

            //creating the prep list and the intermediate standard
            //inventoryItem.MSDS.Add(model.MSDS);
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

            //if (!string.IsNullOrEmpty(submit) && submit.Equals("Save")) {
            //    //save pressed
            //    return RedirectToAction("Index");
            //} else {
            //    //save & new pressed
            //    return RedirectToAction("Create");
            //}
            //}
            //return View(model);
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

            //model.BalanceDevices = devices.Where(item => item.DeviceType.Equals("Balance")).ToList();
            //model.VolumetricDevices = devices.Where(item => item.DeviceType.Equals("Volumetric")).ToList();

            return model;
        }
    }
}
