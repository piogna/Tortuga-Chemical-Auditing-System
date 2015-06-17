using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeStockReagentRepository : IRepository<StockReagent> {
        private List<StockReagent> _reagents = new List<StockReagent>() {
            new StockReagent() { 
                ReagentId = 1,
                IdCode = "09876RGT",
                DateEntered = new DateTime(2015, 03, 25),
                ReagentName = "Stock Reagent One",
                EnteredBy = "DK"
            },
            new StockReagent() { 
                ReagentId = 2,
                IdCode = "RGT09877",
                DateEntered = new DateTime(2015, 03, 22),
                ReagentName = "Stock Reagent Two",
                EnteredBy = "PI"
            }
        };

        public IEnumerable<StockReagent> Get() {
            return _reagents;
        }

        public StockReagent Get(int? i) {
            return _reagents
                .Where(item => item.ReagentId == i)
                .Select(item => item)
                .FirstOrDefault<StockReagent>();
        }

        public void Create(StockReagent t) {
            _reagents.Add(t);
        }

        public void Update(StockReagent t) {
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