using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using System;
using TMNT.Models.Enums;
using System.Data;

namespace TMNT.Models.Repository {
    public class InventoryItemRepository : IRepository<InventoryItem> {
        private ApplicationDbContext db;

        public InventoryItemRepository() { }

        public InventoryItemRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<InventoryItem> Get() {
            return db.InventoryItems.ToList();
        }

        public InventoryItem Get(int? i) {
            return db.InventoryItems.Find(i);
        }

        public void Create(InventoryItem t) {
            db.InventoryItems.Add(t);
        }

        public void Update(InventoryItem t) {
            db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int? i) {
            db.InventoryItems.Remove(db.InventoryItems.Find(i));//change to archive in the future?
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}