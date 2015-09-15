using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class StockReagent {
        public StockReagent() {
            InventoryItems = new List<InventoryItem>();
        }

        [Key]
        public int ReagentId { get; set; }
        //[Required]
        public string LotNumber { get; set; }
        //[Required]
        public string IdCode { get; set; }
        //[Required]
        public string ReagentName { get; set; }
        public double LowAmountThreshHold { get; set; }
        public string LastModifiedBy { get; set; }

        //foreign keys
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
