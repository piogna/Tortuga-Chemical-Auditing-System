using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;

namespace TMNT.Models.Repository {
    public class PreparedReagentRepository : IRepository<PreparedReagent> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public PreparedReagentRepository() { }
        public PreparedReagentRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<PreparedReagent> Get() {
            return db.PreparedReagent.ToList();
        }

        public PreparedReagent Get(int? i) {
            return db.PreparedReagent.Find(i);
        }

        public CheckModelState Create(PreparedReagent t) {
            try {
                db.PreparedReagent.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Update(PreparedReagent t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.PreparedReagent.Remove(db.PreparedReagent.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}