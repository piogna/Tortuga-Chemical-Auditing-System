using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class IntermediateStandardRepository : IRepository<IntermediateStandard> {
        private ApplicationDbContext _db = DbContextSingleton.Instance;

        public IntermediateStandardRepository() { }

        public IntermediateStandardRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<IntermediateStandard> Get() {
            return _db.IntermediateStandards.ToList();
        }

        public IntermediateStandard Get(int? i) {
            return _db.IntermediateStandards.Find(i);
        }

        public void Create(IntermediateStandard t) {
            _db.IntermediateStandards.Add(t);
            _db.SaveChanges();
        }

        public void Update(IntermediateStandard t) {
            _db.Entry(t).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? i) {
            _db.IntermediateStandards.Remove(_db.IntermediateStandards.Find(i));
            _db.SaveChanges();
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}