using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Filters;
using TMNT.Helpers;
using TMNT.Models;
using TMNT.Models.Repository;
using TMNT.Models.ViewModels;
using TMNT.Utils;

namespace TMNT.Controllers {
    [Authorize]
    [PasswordChange]
    public class VolumetricController : Controller {

        private IRepository<Device> repo;
        public VolumetricController()
            : this(new DeviceRepository(DbContextSingleton.Instance)) {
        }

        public VolumetricController(IRepository<Device> repo) {
            this.repo = repo;
        }

        // GET: Volumetric
        [Route("Volumetrics")]
        public ActionResult Index() {
            var department = HelperMethods.GetUserDepartment();

            var volumetrics = repo.Get().Where(item => item.DeviceType.Equals("Volumetric") && item.Department.Equals(department));
            var viewModels = new List<VolumetricIndexViewModel>();

            foreach (var item in volumetrics) {
                viewModels.Add(new VolumetricIndexViewModel() {
                    VolumetricId = item.DeviceId,
                    DeviceCode = item.DeviceCode,
                    IsVerified = item.IsVerified,
                    Department = item.Department,
                    LastVerifiedBy = item.DeviceVerifications//last verified by
                                .Where(x => x.Device.Equals(item))
                                .Count() == 0 ?
                                    null :
                                    item.DeviceVerifications
                                        .Where(x => x.Device.Equals(item))
                                        .OrderBy(x => x.VerifiedOn)
                                        .Select(x => x.User.UserName)
                                        .First()
                });
            }
            return View(viewModels);
        }

        // GET: Volumetric/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = repo.Get(id);
            if (device == null) {
                return HttpNotFound();
            }
            return View(device);
        }

        // GET: Volumetric/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Volumetric/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeviceId,DeviceCode,IsVerified,DeviceType,Status")] Device device) {
            if (ModelState.IsValid) {
                repo.Create(device);
                return RedirectToAction("Index");
            }

            return View(device);
        }

        // GET: Volumetric/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = repo.Get(id);
            if (device == null) {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Volumetric/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeviceId,DeviceCode,IsVerified,DeviceType,Status")] Device device) {
            if (ModelState.IsValid) {
                repo.Update(device);
                return RedirectToAction("Index");
            }
            return View(device);
        }

        // GET: Volumetric/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = repo.Get(id);
            if (device == null) {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Volumetric/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            repo.Delete(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing) {
        //    if (disposing) {
        //        repo.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
