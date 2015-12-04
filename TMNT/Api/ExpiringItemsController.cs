using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Api {
    public class ExpiringItemsController : ApiController {
        ApplicationDbContext db = ApplicationDbContext.Create();
        private UnitOfWork _uow;

        public ExpiringItemsController(UnitOfWork uow) {
            _uow = uow;
        }

        public ExpiringItemsController() : this(new UnitOfWork()) {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
        }

        [ResponseType(typeof(InventoryItem))]
        public IHttpActionResult GetExpiringChemicals() {
            var userDepartment = _uow.GetUserDepartment();

            var chemicals = db.InventoryItems
                .Select(item => new InventoryLowStockApiModel() {
                    InventoryItemId = item.InventoryItemId,
                    Department = item.Department,
                    StockStandard = item.StockStandard,
                    StockReagent = item.StockReagent,
                    IntermediateStandard = item.IntermediateStandard,
                    WorkingStandard = item.WorkingStandard,
                    ChemicalType = item.Type
                })
                .ToList()
                .Where(item => item.StockReagent != null && item.Department.DepartmentId == userDepartment.DepartmentId && item.StockReagent.ExpiryDate < DateTime.Today.AddDays(30) && !(item.StockReagent.ExpiryDate < DateTime.Today) ||
                                item.StockStandard != null && item.Department.DepartmentId == userDepartment.DepartmentId && item.StockStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(item.StockStandard.ExpiryDate < DateTime.Today) ||
                                item.IntermediateStandard != null && item.Department.DepartmentId == userDepartment.DepartmentId && item.IntermediateStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(item.IntermediateStandard.ExpiryDate < DateTime.Today) ||
                                item.WorkingStandard != null && item.Department.DepartmentId == userDepartment.DepartmentId && item.WorkingStandard.ExpiryDate < DateTime.Today.AddDays(30) && !(item.WorkingStandard.ExpiryDate < DateTime.Today)
                                );

            return Ok(chemicals);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }

    }

    public class InventoryLowStockApiModel {
        public int InventoryItemId { get; set; }
        public string ChemicalType { get; set; }

        public StockStandard StockStandard { get; set; }
        public StockReagent StockReagent { get; set; }
        public IntermediateStandard IntermediateStandard { get; set; }
        public WorkingStandard WorkingStandard { get; set; }
        public Department Department { get; set; }
    }
}
