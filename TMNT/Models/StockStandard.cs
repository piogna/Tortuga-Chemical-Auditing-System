using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class StockStandard {
        public StockStandard(){
            InventoryItems = new List<InventoryItem>();
        }
        [Key]
        public int StockStandardId { get; set; }
        public string LotNumber { get; set; }
        public string IdCode { get; set; }
        public string StockStandardName { get; set; }
        public string SolventUsed { get; set; }
        public string SolventSupplierName { get; set; }
        public double Purity { get; set; }
        public string LastModifiedBy { get; set; }
        public string Concentration { get; set; }

        //foreign keys
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}