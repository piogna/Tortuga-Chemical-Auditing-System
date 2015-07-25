using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TMNT.Models.Repository {
    public class StockStandardApiRepository : IApiRepository<StockStandard> {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        public IEnumerable<StockStandard> Get() {
            throw new NotImplementedException();
        }

        public StockStandard Get(string idCode) {
            return db.StockStandards.Where(s => s.IdCode == idCode.ToString()).OrderByDescending(s => s.DateEntered).First();
        }

        public void Create(StockStandard t) {
            throw new NotImplementedException();
        }

        public void Update(StockStandard t) {
            throw new NotImplementedException();
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}