using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class IntermediateStandard {
        [Key]
        public int IntermediateStandardId { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        public string Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }
        public int TotalVolume { get; set; }
        [Display(Name = "Final Concentration")]
        public int FinalConcentration { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        //foreign keys
        [Required]
        public virtual PrepList PrepList { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}