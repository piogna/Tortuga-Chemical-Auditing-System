using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;

namespace TMNT.Controllers {
    public class FeedbackController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ideas
        [Route("Feedback")]
        public ActionResult Index() {
            return View(db.Feedback.ToList());
        }

        // GET: Ideas/Create
        [Route("Feedback/Create")]
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
        public ActionResult Create([Bind(Include = "FeedbackId,Category,Comment")] Feedback feedback) {
            if (ModelState.IsValid) {
                feedback.CreatedBy = Helpers.HelperMethods.GetCurrentUser().UserName;
                db.Feedback.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
