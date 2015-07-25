using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;
using Microsoft.AspNet.Identity;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    public class IntermediateStandardController : Controller {
        private IRepository<IntermediateStandard> repo;

        public IntermediateStandardController() : this(new IntermediateStandardRepository()) {

        }

        public IntermediateStandardController(IRepository<IntermediateStandard> repo) {
            this.repo = repo;
        }

        // GET: /IntermediateStandard/
        [Route("get/all-intermediate-standards")]
        public ActionResult Index() {
            return View(repo.Get());
        }

        // GET: /IntermediateStandard/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntermediateStandard intermediatestandard = repo.Get(id);//db.IntermediateStandards.Find(id);
            if (intermediatestandard == null) {
                return HttpNotFound();
            }
            return View(intermediatestandard);
        }

        [Route("create/new-intermediate-standard")]
        // GET: /IntermediateStandard/Create
        public ActionResult Create() {
            var units = new List<string>() { "Reagent", "Standard", "Intermediate Standard" };
            ViewBag.Types = units;
            return View();
        }

        // POST: /IntermediateStandard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("create/new-intermediate-standard")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IntermediateStandardId,DateCreated")] IntermediateStandardViewModel intermediatestandard, string submit) {
            List<string> amounts = Request.Form.GetValues("amount").Where(item => !string.IsNullOrEmpty(item)).ToList();
            List<string> lotNumbers = Request.Form.GetValues("lotnumber").Where(item => !string.IsNullOrEmpty(item)).ToList();
            List<string> types = Request.Form.GetValues("type").Where(item => !item.Equals("Nothing Selected")).ToList();
            List<StockReagent> reagents = null;

            foreach (var type in types) {
                foreach (var lotNumber in lotNumbers) {
                    if (type == "Reagent") {
                        reagents = new StockReagentRepository(DbContextSingleton.Instance)
                            .Get()
                            .Where(x => x.IdCode == lotNumber)
                            .ToList<StockReagent>();
                    }
                }
            }

            foreach (var reagent in reagents) {
                var inventoryItem = new InventoryItemRepository(DbContextSingleton.Instance)
                                    .Get()
                                    .Where(x => x.StockReagent.ReagentId == reagent.ReagentId)
                                    .FirstOrDefault();
                inventoryItem.Amount -= Convert.ToInt32(amounts[0]);
                new InventoryItemRepository().Update(inventoryItem);
            }

            intermediatestandard.PrepList = new PrepList() {  };
            var errors = ModelState.Where(item => item.Value.Errors.Any());
            if (ModelState.IsValid) {
                //db.IntermediateStandards.Add(intermediatestandard);
                //db.SaveChanges();
                //repo.Create(intermediatestandard);

                if (!string.IsNullOrEmpty(submit) && submit.Equals("Save")) {
                    //save pressed
                    return RedirectToAction("Index");// View("Index");
                } else {
                    //save & new pressed
                    return RedirectToAction("Create");
                }

                //return RedirectToAction("Index");
            }
            return View(intermediatestandard);
        }

        // GET: /IntermediateStandard/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntermediateStandard intermediatestandard = repo.Get(id);//db.IntermediateStandards.Find(id);
            if (intermediatestandard == null) {
                return HttpNotFound();
            }
            return View(intermediatestandard);
        }

        // POST: /IntermediateStandard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IntermediateStandardId,DateCreated,DiscardDate,Replaces,ReplacedBy")] IntermediateStandard intermediatestandard) {
            if (ModelState.IsValid) {
                //db.Entry(intermediatestandard).State = EntityState.Modified;
                //db.SaveChanges();
                repo.Update(intermediatestandard);
                return RedirectToAction("Index");
            }
            return View(intermediatestandard);
        }

        // GET: /IntermediateStandard/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IntermediateStandard intermediatestandard = repo.Get(id);//db.IntermediateStandards.Find(id);
            if (intermediatestandard == null) {
                return HttpNotFound();
            }
            return View(intermediatestandard);
        }

        // POST: /IntermediateStandard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            IntermediateStandard intermediatestandard = repo.Get(id);//db.IntermediateStandards.Find(id);
            //db.IntermediateStandards.Remove(intermediatestandard);
            //db.SaveChanges();
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
