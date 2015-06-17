using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.FakeModels;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeLowStockRepository : IRepository<FakeLowStockStandards> {

        //putting this here so if you wish to update the list, it will save it until the demo is terminated
        private List<FakeLowStockStandards> _standards = new List<FakeLowStockStandards>() {
                new FakeLowStockStandards() { ItemID = 4, ItemType = "Standard", AmountRemaining = 30, UnitType = "mL", SupplierName = "Supplier One", LotNo = "XYZ123" },
                new FakeLowStockStandards() { ItemID = 5, ItemType = "Reagent", AmountRemaining = 45, UnitType = "uG", SupplierName = "Supplier Two", LotNo = "XYZ456" },
                new FakeLowStockStandards() { ItemID = 6, ItemType = "Standard", AmountRemaining = 50, UnitType = "mL", SupplierName = "Supplier Three", LotNo = "XYZ789" }
            };

        public IEnumerable<FakeLowStockStandards> Get() {
            return _standards;
        }

        public FakeLowStockStandards Get(int? i) {
            throw new NotImplementedException();
        }

        public void Create(FakeLowStockStandards t) {
            throw new NotImplementedException();
        }

        public void Update(FakeLowStockStandards t) {
            var updateItem = _standards.FirstOrDefault(item => item.ItemID == t.ItemID);

            if (updateItem != null) {
                updateItem.AmountRemaining = t.AmountRemaining;
            }
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}