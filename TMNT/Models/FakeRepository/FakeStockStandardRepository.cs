using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models.Repository;

namespace TMNT.Models.FakeRepository {
    public class FakeStockStandardRepository : IRepository<StockStandard> {
        //creating a standard list of 3 StockStandard objects. nothing fancy.
        private List<StockStandard> _standards = new List<StockStandard>() {
            new StockStandard() { 
                StockStandardId = 1,
                IdCode = "35790AA",
                StockStandardName = "Stock Standard One",
                DateEntered = new DateTime(2015,02,20),
                Purity = 88.5,
                SolventUsed = "Solvent One",
                InventoryItems = new List<InventoryItem>() {
                    new InventoryItem() { Grade = 90, Amount = 750, CatalogueCode = "12345AB" }
                }
            },
            new StockStandard() { 
                StockStandardId = 2,
                IdCode = "35790AB",
                StockStandardName = "Stock Standard Two",
                DateEntered = new DateTime(2015,02,21),
                Purity = 85.2,
                SolventUsed = "Solvent Two"
            },
            new StockStandard() { 
                StockStandardId = 3,
                IdCode = "35790AB",
                StockStandardName = "Stock Standard Three",
                DateEntered = new DateTime(2015,03,15),
                Purity = 95.2,
                SolventUsed = "Solvent Three"
            }
        };

        public IEnumerable<StockStandard> Get() {
            return _standards;
        }

        public StockStandard Get(int? i) {
            StockStandard standard = _standards
                .Where(item => item.StockStandardId == i)
                .Select(item => item)
                .FirstOrDefault<StockStandard>();
            return standard;
        }

        public void Create(StockStandard t) {
            _standards.Add(t);
        }

        public void Update(StockStandard t) {
            var standard = _standards
                .Where(item => item.IdCode == item.IdCode)
                .FirstOrDefault<StockStandard>();

            if (standard != null) {
                foreach (var item in standard.InventoryItems) {
                    foreach (var replace in t.InventoryItems) {
                        item.Amount = replace.Amount;
                        item.Grade = replace.Grade;
                        item.CatalogueCode = replace.CatalogueCode;
                    }
                }
            }
        }

        public void Delete(int? i) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}