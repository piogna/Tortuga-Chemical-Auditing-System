using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMNT.Models;
using TMNT.Models.Repository;

namespace TMNT.Utils {
    public static class BuildIntermediateStandard {
        public static void UpdateInventoryWithGenerics(List<object> types, List<string> amounts) {
            var inventoryItems = new InventoryItemRepository(DbContextSingleton.Instance).Get();
            int counter = 0;
            bool flag = true;
            while (flag) {
                foreach (var inventoryItem in inventoryItems) {
                    foreach (var type in types) {
                        if (type is StockReagent && inventoryItem.StockReagent != null) {
                            inventoryItem.Amount -= Convert.ToInt32(amounts[counter]);
                            new InventoryItemRepository(DbContextSingleton.Instance).Update(inventoryItem);
                            counter++;
                            if (counter == types.Count) { flag = false; }
                            break;
                        } else if (type is StockStandard && inventoryItem.StockStandard != null) {
                            inventoryItem.Amount -= Convert.ToInt32(amounts[counter]);
                            new InventoryItemRepository(DbContextSingleton.Instance).Update(inventoryItem);
                            counter++;
                            if (counter == types.Count) { flag = false; }
                            break;
                        }
                    }
                    if (!flag) { break; }
                }
            }
        }
    }
}