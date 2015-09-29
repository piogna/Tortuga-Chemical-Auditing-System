using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TMNT.Models.Enums;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeExpiringStandardsRepository : IRepository<FakeExpiringStandards> {

        public IEnumerable<FakeExpiringStandards> Get() {
            return new List<FakeExpiringStandards>() {
                new FakeExpiringStandards() { ItemID = 1, ItemType = "Standard", LotNo = "XYZ123", SupplierName = "Supplier One", ExpiryDate = "2015-04-25" },
                new FakeExpiringStandards() { ItemID = 2, ItemType = "Reagent", LotNo = "FGH765", SupplierName = "Supplier Two", ExpiryDate = "2015-04-25" },
                new FakeExpiringStandards() { ItemID = 3, ItemType = "Standard", LotNo = "OKJ673", SupplierName = "Supplier Three", ExpiryDate = "2015-04-25" }
            };
        }

        public FakeExpiringStandards Get(int? i) {
            throw new NotImplementedException();
        }

        public CheckModelState Create(FakeExpiringStandards t) {
            throw new NotImplementedException();
        }

        public void Update(FakeExpiringStandards t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}