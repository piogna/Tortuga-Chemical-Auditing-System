using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;
using TMNT.Models.Enums;

namespace TMNT.Models.Repository {
    public class MaxxamMadeReagentRepository : IRepository<MaxxamMadeReagent> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public MaxxamMadeReagentRepository() { }
        public MaxxamMadeReagentRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<MaxxamMadeReagent> Get() {
            return db.MaxxamMadeReagent.ToList();
        }

        public MaxxamMadeReagent Get(int? i) {
            return db.MaxxamMadeReagent.Find(i);
        }

        public CheckModelState Create(MaxxamMadeReagent t) {
            try {
                db.MaxxamMadeReagent.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
        }

        public void Update(MaxxamMadeReagent t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.MaxxamMadeReagent.Remove(db.MaxxamMadeReagent.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}