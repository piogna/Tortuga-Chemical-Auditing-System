using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class PreparedReagent {
        public PreparedReagent() {
            InventoryItems = new List<InventoryItem>();
        }
        [Key]
        public int PreparedReagentId { get; set; }
        public string MaxxamId { get; set; }
        public string IdCode { get; set; }
        public string PreparedReagentName { get; set; }
        public string LastModifiedBy { get; set; }

        //foreign keys
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}