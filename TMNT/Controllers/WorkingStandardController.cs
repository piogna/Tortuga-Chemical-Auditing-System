using System.Net;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Controllers {
    public class WorkingStandardController : Controller {
        private IRepository<WorkingStandard> repo;

        public WorkingStandardController() : this(new WorkingStandardRepository()) {

        }

        public WorkingStandardController(IRepository<WorkingStandard> repo) {
            this.repo = repo;
        }

        // GET: /WorkingStandard/
        [Route("WorkingStandard")]
        public ActionResult Index() {
            return View(repo.Get());//db.WorkingStandards.ToList());
        }

        // GET: /WorkingStandard/Details/5
        [Route("WorkingStandard/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingStandard workingstandard = repo.Get(id);//db.WorkingStandards.Find(id);
            if (workingstandard == null) {
                return HttpNotFound();
            }
            return View(workingstandard);
        }

        [Route("WorkingStandard/Create")]
        // GET: /WorkingStandard/Create
        public ActionResult Create() {
            return View();
        }

        // POST: /WorkingStandard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("WorkingStandard/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkingStandardId,PreparationDate,Source,Grade")] WorkingStandard workingstandard) {
            if (ModelState.IsValid) {
                //db.WorkingStandards.Add(workingstandard);
                //db.SaveChanges();
                //repo.Create(workingstandard);
                return View(workingstandard);//return RedirectToAction("Index");
            }

            return View(workingstandard);
        }

        // GET: /WorkingStandard/Edit/5
        [Route("WorkingStandard/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingStandard workingstandard = repo.Get(id);//db.WorkingStandards.Find(id);
            if (workingstandard == null) {
                return HttpNotFound();
            }
            return View(workingstandard);
        }

        // POST: /WorkingStandard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("WorkingStandard/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkingStandardId,PreparationDate,Source,Grade")] WorkingStandard workingstandard) {
            if (ModelState.IsValid) {
                //db.Entry(workingstandard).State = EntityState.Modified;
                //db.SaveChanges();
                repo.Update(workingstandard);
                return RedirectToAction("Index");
            }
            return View(workingstandard);
        }

        // GET: /WorkingStandard/Delete/5
        [Route("WorkingStandard/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkingStandard workingstandard = repo.Get(id);//db.WorkingStandards.Find(id);
            if (workingstandard == null) {
                return HttpNotFound();
            }
            return View(workingstandard);
        }

        // POST: /WorkingStandard/Delete/5
        [Route("WorkingStandard/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            //WorkingStandard workingstandard = db.WorkingStandards.Find(id);
            //db.WorkingStandards.Remove(workingstandard);
            //db.SaveChanges();
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
