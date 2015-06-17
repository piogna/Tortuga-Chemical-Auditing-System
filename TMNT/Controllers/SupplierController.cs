using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.FakeRepository;
using TMNT.Models.Repository;

namespace TMNT.Controllers {
    public class SupplierController : Controller {
        private IRepository<Supplier> repo;

        public SupplierController() : this(new FakeSupplierRepository()) {

        }

        public SupplierController(IRepository<Supplier> repo) {
            this.repo = repo;
        }

        // GET: /Supplier/
        public ActionResult Index() {
            return View(repo.Get());
        }

        // GET: /Supplier/Details/5
        public ActionResult Details() { 
        /*
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = repo.Get(id);//db.Suppliers.Find(id);
            if (supplier == null) {
                return HttpNotFound();
            }
            return View(supplier);
         * */
            return View();
        }

        // GET: /Supplier/Create
        public ActionResult Create() {
            return View();
        }

        // POST: /Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,Name,Address,City,Province,PostalCode,PhoneNumber,FaxNumber,Website")] Supplier supplier) {
            if (ModelState.IsValid) {
                //db.Suppliers.Add(supplier);
                //db.SaveChanges();
                repo.Create(supplier);
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: /Supplier/Edit/5
        public ActionResult Edit() { 
        /*
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = repo.Get(id);//db.Suppliers.Find(id);
            if (supplier == null) {
                return HttpNotFound();
            }
            return View(supplier);
         */
            return View();
        }

        // POST: /Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierId,Name,Address,City,Province,PostalCode,PhoneNumber,Fax,Website")] Supplier supplier) {
            if (ModelState.IsValid) {
                //db.Entry(supplier).State = EntityState.Modified;
                //db.SaveChanges();
                repo.Update(supplier);
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: /Supplier/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = repo.Get(id);//db.Suppliers.Find(id);
            if (supplier == null) {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: /Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            //Supplier supplier = db.Suppliers.Find(id);
            //db.Suppliers.Remove(supplier);
            //db.SaveChanges();
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
