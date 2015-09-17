using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class StockReagent {
        public StockReagent() {
            InventoryItems = new List<InventoryItem>();
        }
        [Key]
        public int ReagentId { get; set; }
        public string LotNumber { get; set; }
        public string IdCode { get; set; }
        public string ReagentName { get; set; }
        public string LastModifiedBy { get; set; }

        //foreign keys
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
