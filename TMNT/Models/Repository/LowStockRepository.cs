using System;
using System.Collections.Generic;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class LowStockRepository : IRepository<InventoryItem> {
        private ApplicationDbContext db = DbContextSingleton.Instance;


        public IEnumerable<InventoryItem> Get() {
            throw new NotImplementedException();
        }

        public InventoryItem Get(int? i) {
            throw new NotImplementedException();
        }

        public CheckModelState Create(InventoryItem t) {
            throw new NotImplementedException();
        }

        public void Update(InventoryItem t) {
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