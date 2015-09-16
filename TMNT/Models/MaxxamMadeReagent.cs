using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class MaxxamMadeReagent {
        public MaxxamMadeReagent() {
            InventoryItems = new List<InventoryItem>();
        }
        [Key]
        public int MaxxamMadeReagentId { get; set; }
        public string LotNumber { get; set; }
        public string IdCode { get; set; }
        public string MaxxamMadeReagentName { get; set; }
        public string LastModifiedBy { get; set; }

        //foreign keys
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}