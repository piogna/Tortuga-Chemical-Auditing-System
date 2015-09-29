using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TMNT.Utils;
using System;
using TMNT.Models.Enums;

namespace TMNT.Models.Repository {
    public class StockReagentRepository : IRepository<StockReagent> {
        private ApplicationDbContext db = DbContextSingleton.Instance;

        public StockReagentRepository() { }

        public StockReagentRepository(ApplicationDbContext db) {
            this.db = db;
        }

        public IEnumerable<StockReagent> Get() {
            return db.StockReagents.ToList();
        }

        public StockReagent Get(int? i) {
            return db.StockReagents.Find(i);
        }

        public CheckModelState Create(StockReagent t) {
            try {
                db.StockReagents.Add(t);
                if (db.SaveChanges() > 0) {
                    return CheckModelState.Valid;
                }
            } catch (Exception ex) {

            }
            return CheckModelState.Invalid;
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