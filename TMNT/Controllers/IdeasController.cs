﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;

namespace TMNT.Controllers {
    public class IdeasController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ideas
        [Route("Ideas")]
        public ActionResult Index() {
            return View(db.Ideas.ToList());
        }

        // GET: Ideas/Create
        [Route("Ideas/Create")]
        public ActionResult Create() {
            ViewBag.Categories = new List<string>() { "Idea", "Concern", "Problem" };
            return View();
        }

        // POST: Ideas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ideas/Create")]
        public ActionResult Create([Bind(Include = "IdeaId,Category,Comment")] Ideas ideas) {
            if (ModelState.IsValid) {
                ideas.CreatedBy = Helpers.HelperMethods.GetCurrentUser().UserName;
                db.Ideas.Add(ideas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ideas);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
