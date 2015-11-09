using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Models.Enums;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    /* Not using the database since units will remain static. List for better performance */
    public class UnitRepository : IRepository<Unit> {
        private ApplicationDbContext _db = DbContextSingleton.Instance;

        public UnitRepository() { }

        public UnitRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<Unit> Get() {
            return _db.Units.ToList();
        }

        public Unit Get(int? i) {
            return _db.Units.Find(i);
        }

        public CheckModelState Create(Unit t) {
            throw new NotImplementedException();
        }

        public CheckModelState Update(Unit t) {
            throw new NotImplementedException();
        }

        public CheckModelState Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}