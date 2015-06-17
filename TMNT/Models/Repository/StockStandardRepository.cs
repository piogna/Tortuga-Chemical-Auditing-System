using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TMNT.Models.Repository {
    public class StockStandardRepository : IRepository<StockStandard> {
        private ApplicationDbContext db = ApplicationDbContext.Create();

        public IEnumerable<StockStandard> Get() {
            return db.StockStandards
                .Include("InventoryItems.CertificatesOfAnalysis")
                .ToList();
        }

        public StockStandard Get(int? i) {
            return db.StockStandards.Find(i);
        }

        public void Create(StockStandard t) {
            db.StockStandards.Add(t);
            db.SaveChanges();
        }

        public void Update(StockStandard t) {
            db.Entry(t).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int? i) {
            db.StockStandards.Remove(db.StockStandards.Find(i));
            db.SaveChanges();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}