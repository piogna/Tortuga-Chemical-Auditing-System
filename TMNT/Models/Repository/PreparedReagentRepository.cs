using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;
using System.Data;

namespace TMNT.Models.Repository {
    public class PreparedReagentRepository : IRepository<PreparedReagent> {
        private ApplicationDbContext _db;

        public PreparedReagentRepository() { }
        public PreparedReagentRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<PreparedReagent> Get() {
            return _db.PreparedReagent.ToList();
        }

        public PreparedReagent Get(int? i) {
            return _db.PreparedReagent.Find(i);
        }

        public void Create(PreparedReagent t) {
            _db.PreparedReagent.Add(t);
        }

        public void Update(PreparedReagent t) {
            _db.Entry(t).State = EntityState.Modified;

        }

        public void Delete(int? i) {
            _db.PreparedReagent.Remove(_db.PreparedReagent.Find(i));//change to archive in the future?
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}