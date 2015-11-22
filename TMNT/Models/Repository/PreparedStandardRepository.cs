using System;
using System.Collections.Generic;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;
using System.Data;

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

        public void Create(PreparedStandard t) {
                db.PreparedStandard.Add(t);

        }

        public void Update(PreparedStandard t) {
                db.Entry(t).State = EntityState.Modified;

        }

        public void Delete(int? i) {
                db.StockReagents.Remove(db.StockReagents.Find(i));//change to archive in the future?

        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}