using System;
using System.Collections.Generic;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class LowStockRepository : IRepository<InventoryItem> {
        private ApplicationDbContext _db;

        public LowStockRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<InventoryItem> Get() {
            throw new NotImplementedException();
        }

        public InventoryItem Get(int? i) {
            throw new NotImplementedException();
        }

        public void Create(InventoryItem t) {
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