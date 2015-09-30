using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TMNT.Helpers;
using TMNT.Models;
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
            var intermediatestandards = repo.Get();
            var list = new List<IntermediateStandardIndexViewModel>();

            foreach (var item in intermediatestandards) {
                list.Add(new IntermediateStandardIndexViewModel() {
                    IntermediateStandardId = item.IntermediateStandardId,
                    IdCode = item.IdCode,
                    MaxxamId = item.MaxxamId
                });
            }
            //iterating through the associated InventoryItem and retrieving the appropriate data
            //this is faster than LINQ
            int counter = 0;
            foreach (var standard in intermediatestandards) {
                foreach (var invItem in standard.InventoryItems) {
                    if (standard.IntermediateStandardId == invItem.IntermediateStandard.IntermediateStandardId) {
                        list[counter].ExpiryDate = invItem.ExpiryDate;
                        list[counter].DateCreated = invItem.DateCreated;
                        list[counter].CreatedBy = invItem.CreatedBy;
                    }
                }
            }
            return View(list);
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

            var vIntermediateStandard = new IntermediateStandardDetailsViewModel() {
                IntermediateStandardId = intermediatestandard.IntermediateStandardId,
                Replaces = intermediatestandard.Replaces,
                ReplacedBy = intermediatestandard.ReplacedBy,
                PrepList = intermediatestandard.PrepList,
                PrepListItems = intermediatestandard.PrepList.PrepListItems.ToList(),
                IdCode = intermediatestandard.IdCode,
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
            var units = new UnitRepository().Get();
            var model = new IntermediateStandardCreateViewModel();

            model.WeightUnits = units.Where(item => item.UnitType.Equals("Weight")).ToList();
            model.VolumetricUnits = units.Where(item => item.UnitType.Equals("Volume")).ToList();
            model.OtherUnit = units.Where(item => item.UnitType.Equals("Other")).FirstOrDefault();

            return View(model);
        }

        // POST: /IntermediateStandard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("IntermediateStandard/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IntermediateStandardId,TotalVolume,UsedFor,MaxxamId,FinalConcentration,FinalVolume,TotalAmount,ExpiryDate,IdCode")] IntermediateStandardCreateViewModel intermediatestandard, string submit) {
            var errors = ModelState.Where(item => item.Value.Errors.Any());
            if (ModelState.IsValid) {
                //retrieving all table rows from recipe builder - replace with view model in the future
                IntermediateStandardPrepListItemsViewModel prepListViewModel = new IntermediateStandardPrepListItemsViewModel() {
                    AmountsWithUnits = Request.Form.GetValues("Amount").Where(item => !string.IsNullOrEmpty(item)).ToArray()
                };

                prepListViewModel.Amounts = prepListViewModel.AmountsWithUnits.Select(item => item.Split(' ')[0]).ToArray();
                prepListViewModel.Units = prepListViewModel.AmountsWithUnits.Select(item => item.Split(' ')[1]).ToArray();
                prepListViewModel.IdCodes = Request.Form.GetValues("IdCodes").Where(item => !string.IsNullOrEmpty(item)).ToArray();
                prepListViewModel.Types = Request.Form.GetValues("Type").Where(item => !item.Equals("Choose Chemical Type")).ToArray();

                //string[] amountsWithUnits = Request.Form.GetValues("Amount").Where(item => !string.IsNullOrEmpty(item)).ToArray();
                //string[] amounts = amountsWithUnits.Select(item => item.Split(' ')[0]).ToArray();
                //string[] idcodes = Request.Form.GetValues("IdCodes").Where(item => !string.IsNullOrEmpty(item)).ToArray();
                //string[] types = Request.Form.GetValues("Type").Where(item => !item.Equals("Choose Chemical Type")).ToArray();
                //List<string> units = Request.Form.GetValues("Unit").Where(item => !string.IsNullOrEmpty(item)).ToList();
                //string[] units = amountsWithUnits.Select(item => item.Split(' ')[1]).ToArray();

                List<object> reagentAndStandardContainer = new List<object>();
                List<PrepListItem> prepItems = new List<PrepListItem>();

                //go through all types and sort out what they are, instantiate, and build list of objects
                foreach (var idcode in prepListViewModel.IdCodes) {
                    foreach (var type in prepListViewModel.Types) {
                        if (type == "Reagent") {
                            var add = new StockReagentRepository(DbContextSingleton.Instance)
                                .Get()
                                .Where(x => x.IdCode == idcode)
                                .FirstOrDefault();
                            if (add != null) { reagentAndStandardContainer.Add(add); break; }
                        } else if (type == "Standard") {
                            var add = new StockStandardRepository(DbContextSingleton.Instance)
                                .Get()
                                .Where(x => x.IdCode == idcode)
                                .FirstOrDefault();
                            if (add != null) { reagentAndStandardContainer.Add(add); break; }
                        } else if (type == "Intermediate Standard") {
                            var add = new IntermediateStandardRepository(DbContextSingleton.Instance)
                                .Get()
                                .Where(x => x.IdCode == idcode)
                                .FirstOrDefault();
                            if (add != null) { reagentAndStandardContainer.Add(add); break; }
                        }
                    }
                }

                //building the prep list with the desired prep list items
                UnitRepository unitRepo = new UnitRepository();
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

                intermediatestandard.PrepList = prepList;

                //building the intermediate standard
                IntermediateStandard standard = new IntermediateStandard() {
                    TotalVolume = intermediatestandard.TotalAmount,
                    FinalConcentration = intermediatestandard.FinalConcentration,
                    FinalVolume = intermediatestandard.FinalVolume,
                    MaxxamId = intermediatestandard.MaxxamId,
                    IdCode = intermediatestandard.IdCode,
                    PrepList = intermediatestandard.PrepList,
                    Replaces = !string.IsNullOrEmpty(intermediatestandard.Replaces) ? intermediatestandard.Replaces : "N/A",
                    ReplacedBy = !string.IsNullOrEmpty(intermediatestandard.ReplacedBy) ? intermediatestandard.ReplacedBy : "N/A"
                };

                InventoryItem inventoryItem = new InventoryItem() {
                    CreatedBy = !string.IsNullOrEmpty(HelperMethods.GetCurrentUser().UserName) ? HelperMethods.GetCurrentUser().UserName : "USERID",
                    DateCreated = DateTime.Today,
                    Department = HelperMethods.GetUserDepartment(),
                    IntermediateStandard = standard,
                    Type = "Intermediate Standard",
                    StorageRequirements = intermediatestandard.StorageRequirements,
                    UsedFor = intermediatestandard.UsedFor,
                    ExpiryDate = intermediatestandard.ExpiryDate
                };

                //creating the prep list and the intermediate standard
                new PrepListRepository(DbContextSingleton.Instance).Create(prepList);
                repo.Create(standard);
                new InventoryItemRepository(DbContextSingleton.Instance).Create(inventoryItem);

                if (!string.IsNullOrEmpty(submit) && submit.Equals("Save")) {
                    //save pressed
                    return RedirectToAction("Index");
                } else {
                    //save & new pressed
                    return RedirectToAction("Create");
                }
            }
            return View(intermediatestandard);
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
                IdCode = intermediatestandard.IdCode
            };

            return View(model);
        }

        // POST: /IntermediateStandard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("IntermediateStandard/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IntermediateStandardId,DateCreated,DiscardDate,Replaces,ReplacedBy")] IntermediateStandard intermediatestandard) {
            if (ModelState.IsValid) {
                repo.Update(intermediatestandard);
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
        [Route("IntermediateStandard/Edit/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            IntermediateStandard intermediatestandard = repo.Get(id);
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
