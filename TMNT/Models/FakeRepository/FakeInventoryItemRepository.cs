using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeInventoryItemRepository : IRepository<InventoryItem> {
        private List<InventoryItem> _items = new List<InventoryItem>() {
                new InventoryItem() {
                    InventoryItemId = 1,
                    CatalogueCode = "ABC123",
                    Amount = 800,
                    Grade = 95,
                    CaseNumber = 1,
                    UsedFor = "Testing Purposes",
                    CreatedBy = "Drew Kennedy",
                    DateCreated = new DateTime(2015, 02, 13),
                    DateModified = new DateTime(2015, 04, 13)
                }
            };

        public IEnumerable<InventoryItem> Get() {
            return _items;
        }

        public InventoryItem Get(int? i) {
            return _items
                .Where(item => item.InventoryItemId == i)
                .FirstOrDefault<InventoryItem>();
        }

        public void Create(InventoryItem t) {
            _items.Add(t);
        }

        public void Update(InventoryItem t) {
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