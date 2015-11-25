using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace TMNT.Models.Repository {
    public class StockStandardRepository : IRepository<StockStandard> {
        private ApplicationDbContext _db;

        public StockStandardRepository() { }

        public StockStandardRepository(ApplicationDbContext db) {
            this._db = db;
        }

        public IEnumerable<StockStandard> Get() {
            return _db.StockStandards
                .Include("InventoryItems.CertificatesOfAnalysis")
                .ToList();
        }

        public StockStandard Get(int? i) {
            return _db.StockStandards.Find(i);
        }

        public void Create(StockStandard t) {
            _db.StockStandards.Add(t);
        }

        public void Update(StockStandard t) {
            _db.Entry(t).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int? i) {
            _db.StockStandards.Remove(_db.StockStandards.Find(i));//change to archive in the future?
        }

        public void Dispose() {

        }
    }
}