using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using System;
using TMNT.Models.Enums;

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

        public CheckModelState Create(InventoryItem t) {
            try {
                db.InventoryItems.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Update(InventoryItem t) {
            try {
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
            } catch (System.Data.Entity.Validation.DbEntityValidationException ex) {

            }
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