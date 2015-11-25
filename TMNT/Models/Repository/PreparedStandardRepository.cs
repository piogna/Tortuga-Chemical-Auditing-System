using System;
using System.Collections.Generic;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;
using System.Data;

namespace TMNT.Models.Repository {
    public class PreparedStandardRepository : IRepository<PreparedStandard> {
        private ApplicationDbContext _db;

        public PreparedStandardRepository() { }

        public PreparedStandardRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<PreparedStandard> Get() {
            return _db.PreparedStandard;
        }

        public PreparedStandard Get(int? i) {
            return _db.PreparedStandard.Find(i);
        }

        public void Create(PreparedStandard t) {
            _db.PreparedStandard.Add(t);
        }

        public void Update(PreparedStandard t) {
            _db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int? i) {
            _db.StockReagents.Remove(_db.StockReagents.Find(i));//change to archive in the future?
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}