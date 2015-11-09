using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using TMNT.Models.Enums;
using System;

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

        public CheckModelState Create(Supplier t) {
            try {
                db.Suppliers.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public CheckModelState Update(Supplier t) {
            throw new NotImplementedException();
        }

        public CheckModelState Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}