using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using System;
using TMNT.Models.Enums;
using System.Data;

namespace TMNT.Models.Repository {
    public class InventoryItemRepository : IRepository<InventoryItem> {
        private ApplicationDbContext _db;

        public InventoryItemRepository() { }

        public InventoryItemRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<InventoryItem> Get() {
            return _db.InventoryItems.ToList();
        }

        public InventoryItem Get(int? i) {
            return _db.InventoryItems.Find(i);
        }

        public void Create(InventoryItem t) {
            _db.InventoryItems.Add(t);
        }

        public void Update(InventoryItem t) {
            _db.Entry(t).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? i) {
            _db.InventoryItems.Remove(_db.InventoryItems.Find(i));//change to archive in the future?
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}