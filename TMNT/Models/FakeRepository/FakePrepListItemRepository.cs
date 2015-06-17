using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakePrepListItemRepository : IRepository<PrepListItem> {
        /*
         * creating a list of PrepList objects. we need three instances for this.
         * 
         */
        private List<PrepListItem> _items = new List<PrepListItem>() {
            new PrepListItem() {
                PrepListItemId = 1,
                Amount = 90,
                StockStandard = new FakeStockStandardRepository().Get(1),//new StockStandard(),
                PrepList = new PrepList(),//new FakePrepListRepository().Get(1),
                IntermediateStandard = null,//new FakeIntermediateStandardRepository().Get(1),
                StockReagent = null
            },
            new PrepListItem() {
                PrepListItemId = 2,
                Amount = 85,
                StockStandard = new FakeStockStandardRepository().Get(2),
                PrepList = new PrepList(),//new FakePrepListRepository().Get(2),
                IntermediateStandard = null,
                StockReagent = null
            },
            new PrepListItem() {
                PrepListItemId = 3,
                Amount = 120,
                StockStandard = null,//new FakeStockStandardRepository().Get(3),
                PrepList = new PrepList(),//new FakePrepListRepository().Get(3),
                IntermediateStandard = new FakeIntermediateStandardRepository().Get(9),
                StockReagent = null
            },
            new PrepListItem() {
                PrepListItemId = 4,
                Amount = 60,
                StockStandard = null,//new FakeStockStandardRepository().Get(3),
                PrepList = new PrepList(),//new FakePrepListRepository().Get(3),
                IntermediateStandard = null,//new FakeIntermediateStandardRepository().Get(9),
                StockReagent = new FakeStockReagentRepository().Get(1)
            }
        };

        public IEnumerable<PrepListItem> Get() {
            return _items;
        }

        public PrepListItem Get(int? i) {
            return _items
                .Where(item => item.PrepListItemId == i)
                .Select(item => item)
                .FirstOrDefault<PrepListItem>();
        }

        public void Create(PrepListItem t) {
            throw new NotImplementedException();
        }

        public void Update(PrepListItem t) {
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