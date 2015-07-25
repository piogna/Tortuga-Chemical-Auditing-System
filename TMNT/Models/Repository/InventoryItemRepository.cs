using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class InventoryItemRepository : IRepository<InventoryItem> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public InventoryItemRepository() { }

        public InventoryItemRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<InventoryItem> Get() {
            var list = db.InventoryItems.ToList();
            return list;
        }

        public InventoryItem Get(int? i) {
            return db.InventoryItems.Find(i);
        }

        public void Create(InventoryItem t) {
            db.InventoryItems.Add(t);
            db.SaveChanges();
        }

        public void Update(InventoryItem t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.InventoryItems.Remove(db.InventoryItems.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}