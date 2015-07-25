using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TMNT.Utils;

namespace TMNT.Models.Repository {
    public class StockReagentRepository : IRepository<StockReagent> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public StockReagentRepository() { }

        public StockReagentRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<StockReagent> Get() {
            var list = db.StockReagents.ToList();
            //db.Dispose();
            return list;
        }

        public StockReagent Get(int? i) {
            return db.StockReagents.Find(i);
        }

        public void Create(StockReagent t) {
            db.StockReagents.Add(t);
            db.SaveChanges();
        }

        public void Update(StockReagent reagent) {
            db.Entry(reagent).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.StockReagents.Remove(db.StockReagents.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            db.Dispose();
        }
    }
}