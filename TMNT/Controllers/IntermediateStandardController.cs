using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
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
            var list = new List<IntermediateStandardViewModel>();

            foreach (var item in intermediatestandards) {
                list.Add(new IntermediateStandardViewModel() {
                    IntermediateStandardId = item.IntermediateStandardId,
                    IdCode = item.IdCode
                });
            }
            //iterating through the associated InventoryItem and retrieving the appropriate data
            //this is faster than LINQ
            int counter = 0;
            foreach (var standard in intermediatestandards) {
                foreach (var invItem in standard.InventoryItems) {
                    if (standard.IntermediateStandardId == invItem.IntermediateStandard.IntermediateStandardId) {
                        list[counter].MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                        list[counter].UsedFor = invItem.UsedFor;
                        list[counter].Unit = invItem.Unit;
                        list[counter].CatalogueCode = invItem.CatalogueCode;
                        list[counter].ExpiryDate = invItem.ExpiryDate;
                        list[counter].IsExpired = invItem.ExpiryDate.Date >= DateTime.Today;
                        list[counter].DateOpened = invItem.DateOpened;
                        list[counter].IsOpened = invItem.DateOpened != null;
                        list[counter].DateCreated = invItem.DateCreated;
                        list[counter].CreatedBy = invItem.CreatedBy;
                        list[counter].DateModified = invItem.DateModified;
                    }
                }
                counter++;
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

            var vIntermediateStandard = new IntermediateStandardViewModel() {
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
                    //vIntermediateStandard.MSDS = invItem.MSDS.Where(x => x.InventoryItem.InventoryItemId == invItem.InventoryItemId).First();
                    vIntermediateStandard.UsedFor = invItem.UsedFor;
                    vIntermediateStandard.CatalogueCode = invItem.CatalogueCode;
                }
            }
            return View(vIntermediateStandard);
        }

        [Route("IntermediateStandard/Create")]
        // GET: /IntermediateStandard/Create
        public ActionResult Create() {
            ViewBag.Storage = new List<string>() { "Fridge", "Freezer", "Shelf" };
            ViewBag.Types = new List<string>() { "Reagent", "Standard", "Intermediate Standard" }; ;
            return View();
        }

        // POST: /IntermediateStandard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("IntermediateStandard/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IntermediateStandardId,DateCreated,InventoryItemName,TotalVolume,UsedFor,CatalogueCode")] IntermediateStandardViewModel intermediatestandard, string submit) {
            //retrieving all rows from recipe builder - replace with view model in the future
            List<string> amounts = Request.Form.GetValues("Amount").Where(item => !string.IsNullOrEmpty(item)).ToList();
            List<string> idcodes = Request.Form.GetValues("IdCode").Where(item => !string.IsNullOrEmpty(item)).ToList();
            List<string> types = Request.Form.GetValues("Type").Where(item => !item.Equals("No Chemical Type Selected")).ToList();

            List<object> reagentAndStandardContainer = new List<object>();
            List<PrepListItem> prepItems = new List<PrepListItem>();

            //go through all types and sort out what they are, instantiate, and build list of objects
            foreach (var idcode in idcodes) {
                foreach (var type in types) {
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
            int counter = 0;
            foreach (var item in reagentAndStandardContainer) {
                if (item is StockReagent) {
                    prepItems.Add(new PrepListItem() {
                        StockReagent = item as StockReagent,
                    });
                } else if (item is StockStandard) {
                    prepItems.Add(new PrepListItem() {
                        StockStandard = item as StockStandard,
                    });
                }
                counter++;
            }

            PrepList prepList = new PrepList() {
                PrepListItems = prepItems
            };

            intermediatestandard.PrepList = prepList;
            intermediatestandard.CreatedBy = string.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name)
                                ? "USERID"
                                : System.Web.HttpContext.Current.User.Identity.Name;

            var errors = ModelState.Where(item => item.Value.Errors.Any());
            if (ModelState.IsValid) {
                //update amounts of reagents, standards, or intermediate standards in the database
                if (reagentAndStandardContainer != null) { BuildIntermediateStandard.UpdateInventoryWithGenerics(reagentAndStandardContainer, amounts); }
                //building the intermediate standard
                IntermediateStandard standard = new IntermediateStandard() {
                    TotalVolume = intermediatestandard.TotalAmount,
                    FinalConcentration = intermediatestandard.FinalConcentration,
                    FinalVolume = intermediatestandard.FinalVolume,
                    MaxxamId = intermediatestandard.MaxxamId,
                    IdCode = intermediatestandard.CatalogueCode,
                    PrepList = intermediatestandard.PrepList,
                    Replaces = string.IsNullOrEmpty(intermediatestandard.Replaces) ? "N/A" : intermediatestandard.Replaces,
                    ReplacedBy = string.IsNullOrEmpty(intermediatestandard.ReplacedBy) ? "N/A" : intermediatestandard.ReplacedBy
                };
                //creating the prep list and the intermediate standard
                new PrepListRepository(DbContextSingleton.Instance).Create(prepList);
                repo.Create(standard);

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

            IntermediateStandardViewModel model = new IntermediateStandardViewModel() {
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
