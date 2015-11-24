using System;
using System.Collections.Generic;
using System.Linq;


namespace TMNT.Models.Repository {
    public class StockStandardApiRepository : IApiRepository<StockStandard> {
        private ApplicationDbContext db;
        public IEnumerable<StockStandard> Get() {
            throw new NotImplementedException();
        }

        public StockStandard Get(int? idCode) {
            throw new NotImplementedException();
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