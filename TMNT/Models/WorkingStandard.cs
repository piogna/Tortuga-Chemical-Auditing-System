using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class WorkingStandard {
        public WorkingStandard() {
            InventoryItems = new List<InventoryItem>();
        }
        [Key]
        public int WorkingStandardId { get; set; }
        [Required]
        public string IdCode { get; set; }
        [Required]
        public string MaxxamId { get; set; }
        public string TotalVolume { get; set; }
        public string FinalConcentration { get; set; }
        public string LastModifiedBy { get; set; }
        public string SafetyNotes { get; set; }

        //shared
        [DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }
        public int? DaysUntilExpired { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateModified { get; set; }
        

        //foreign keys
        [Required]
        public virtual PrepList PrepList { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}