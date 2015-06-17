using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TMNT.Models.Repository {
    public class StockReagentRepository : IRepository<StockReagent> {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        public IEnumerable<StockReagent> Get() {
            return db.StockReagents.ToList();
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
            throw new NotImplementedException();
        }
    }
}