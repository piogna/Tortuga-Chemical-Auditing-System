using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Controllers {
    public class InventoryItemController : Controller {

        private UnitOfWork _uow;

        public InventoryItemController()
            : this(new UnitOfWork()) {

        }

        public InventoryItemController(UnitOfWork uow) {
            _uow = uow;
        }

        // GET: /InventoryItem/
        [Route("InventoryItem")]
        public ActionResult Index() {
            return View(_uow.InventoryItemRepository.Get());
        }

        // GET: /InventoryItem/Details/5
        [Route("InventoryItem/Details/{id?}")]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryitem = _uow.InventoryItemRepository.Get(id);
            if (inventoryitem == null) {
                return HttpNotFound();
            }
            return View(inventoryitem);
        }

        // GET: /InventoryItem/Create
        [Route("InventoryItem/Create")]
        public ActionResult Create() {
            return View();
        }

        // POST: /InventoryItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("InventoryItem/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InventoryItemId,CatalogueCode,InventoryItemName,Size,Grade,CaseNumber,UsedFor,MSDS,CreatedBy,DateCreated")] InventoryItem inventoryitem, HttpPostedFileBase upload) {
            if (ModelState.IsValid) {
                if (upload != null) {
                    var cofa = new CertificateOfAnalysis() {
                        FileName = upload.FileName,
                        ContentType = upload.ContentType,
                        DateAdded = DateTime.Today
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream)) {
                        cofa.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    inventoryitem.CertificatesOfAnalysis.Add(cofa);
                }
                _uow.InventoryItemRepository.Create(inventoryitem);
                return RedirectToAction("Index");
            }

            return View(inventoryitem);
        }

        // GET: /InventoryItem/Edit/5
        [Route("InventoryItem/Edit/{id?}")]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryitem = _uow.InventoryItemRepository.Get(id);
            if (inventoryitem == null) {
                return HttpNotFound();
            }
            return View(inventoryitem);
        }

        // POST: /InventoryItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("InventoryItem/Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventoryItemId,CatalogueCode,InventoryItemName,Size,Grade,CaseNumber,UsedFor,MSDS,CreatedBy,DateCreated")] InventoryItem inventoryitem) {
            if (ModelState.IsValid) {
                _uow.InventoryItemRepository.Update(inventoryitem);
                return RedirectToAction("Index");
            }
            return View(inventoryitem);
        }

        // GET: /InventoryItem/Delete/5
        [Route("InventoryItem/Delete/{id?}")]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryItem inventoryitem = _uow.InventoryItemRepository.Get(id);
            if (inventoryitem == null) {
                return HttpNotFound();
            }
            return View(inventoryitem);
        }

        // POST: /InventoryItem/Delete/5
        [Route("InventoryItem/Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            _uow.InventoryItemRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
