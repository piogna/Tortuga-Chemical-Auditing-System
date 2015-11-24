using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Controllers {
    public class FeedbackController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UnitOfWork _uow = new UnitOfWork();

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
        [Route("Feedback/Create")]
        public ActionResult Create([Bind(Include = "FeedbackId,Category,Comment")] Feedback feedback) {
            if (ModelState.IsValid) {
                feedback.CreatedBy = _uow.GetCurrentUser().UserName;
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
            _uow.Dispose();
            base.Dispose(disposing);
        }
    }
}
