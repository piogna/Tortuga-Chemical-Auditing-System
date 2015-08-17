using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class Unit {
        [Key]
        public int UnitId { get; set; }
        [Required, Display(Name = "Unit Name")]
        public string UnitShorthandName { get; set; }
        [Display(Name = "Unit Name")]
        public string UnitFullName { get; set; }
        [Display(Name = "Unit Type")]
        public string UnitType { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
