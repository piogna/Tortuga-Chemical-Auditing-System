﻿using System;
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
            return View();
        }

        // POST: /IntermediateStandard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IntermediateStandardId,DateCreated,Replaces,ReplacedBy")] IntermediateStandard intermediatestandard) {
            if (ModelState.IsValid) {
                //db.IntermediateStandards.Add(intermediatestandard);
                //db.SaveChanges();
                repo.Create(intermediatestandard);
                return RedirectToAction("Index");
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
