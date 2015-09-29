using System;
using System.Collections.Generic;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;

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

        public CheckModelState Create(MaxxamMadeStandard t) {
            try {
                db.MaxxamMadeStandard.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
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