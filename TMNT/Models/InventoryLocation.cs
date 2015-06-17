using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class InventoryLocation {
        [Key]
        public int InventoryLocationId { get; set; }
        [Required]
        public string InventoryLocationName { get; set; }
        public ICollection<InventoryItem> InventoryItems { get; set; }

        //foreign keys
        [Required]
        public virtual Location Location { get; set; }
    }
}