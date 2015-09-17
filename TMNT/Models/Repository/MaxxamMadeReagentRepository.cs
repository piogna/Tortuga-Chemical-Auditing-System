using System;
using System.Collections.Generic;
using System.Linq;
using TMNT.Utils;
using System.Data.Entity;

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

        public void Create(MaxxamMadeReagent t) {
            db.MaxxamMadeReagent.Add(t);
            db.SaveChanges();
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