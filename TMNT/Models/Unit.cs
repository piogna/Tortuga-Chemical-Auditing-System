using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class Unit {
        [Key]
        public int UnitId { get; set; }
        [Required, Display(Name = "Unit Name")]
        public string UnitName { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
