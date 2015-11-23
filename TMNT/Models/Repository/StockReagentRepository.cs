using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using System;
using TMNT.Models.Enums;
using System.Data;

namespace TMNT.Models.Repository {
    public class StockReagentRepository : IRepository<StockReagent> {
        private ApplicationDbContext _db;

        public StockReagentRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<StockReagent> Get() {
            return _db.StockReagents.ToList();
        }

        public StockReagent Get(int? i) {
            return _db.StockReagents.Find(i);
        }

        public void Create(StockReagent t) {
            _db.StockReagents.Add(t);
        }

        public void Update(StockReagent t) {
            _db.Entry(t).State = EntityState.Modified;
        }

        public void Delete(int? i) {
            _db.StockReagents.Remove(_db.StockReagents.Find(i));//change to archive in the future?
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}