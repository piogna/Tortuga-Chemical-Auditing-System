using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TMNT.Models.Repository {
    public class InventoryItemRepository : IRepository<InventoryItem> {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        public IEnumerable<InventoryItem> Get() {
            return db.InventoryItems;
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
            throw new NotImplementedException();
        }
    }
}