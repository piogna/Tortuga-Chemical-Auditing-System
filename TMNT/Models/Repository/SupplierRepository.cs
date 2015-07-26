using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class SupplierRepository : IRepository<Supplier> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public SupplierRepository() { }

        public SupplierRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<Supplier> Get() {
            return db.Suppliers.ToList();
        }

        public Supplier Get(int? i) {
            return db.Suppliers.Find(i);
        }

        public void Create(Supplier t) {
            db.Suppliers.Add(t);
            db.SaveChanges();
        }

        public void Update(Supplier t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.Suppliers.Remove(db.Suppliers.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}