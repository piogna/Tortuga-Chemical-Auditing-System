using System;
using System.Collections.Generic;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;

namespace TMNT.Models.Repository {
    public class PreparedStandardRepository : IRepository<PreparedStandard> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public PreparedStandardRepository() { }

        public PreparedStandardRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<PreparedStandard> Get() {
            return db.PreparedStandard;
        }

        public PreparedStandard Get(int? i) {
            return db.PreparedStandard.Find(i);
        }

        public CheckModelState Create(PreparedStandard t) {
            try {
                db.PreparedStandard.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Update(PreparedStandard t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.PreparedStandard.Remove(db.PreparedStandard.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}