using System;
using System.Collections.Generic;
using TMNT.Utils;
using System.Data.Entity;

namespace TMNT.Models.Repository {
    public class MaxxamMadeStandardRepository : IRepository<MaxxamMadeStandard> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public MaxxamMadeStandardRepository() { }

        public MaxxamMadeStandardRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<MaxxamMadeStandard> Get() {
            return db.MaxxamMadeStandard;
        }

        public MaxxamMadeStandard Get(int? i) {
            return db.MaxxamMadeStandard.Find(i);
        }

        public void Create(MaxxamMadeStandard t) {
            db.MaxxamMadeStandard.Add(t);
            db.SaveChanges();
        }

        public void Update(MaxxamMadeStandard t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.MaxxamMadeStandard.Remove(db.MaxxamMadeStandard.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}