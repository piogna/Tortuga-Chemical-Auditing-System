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
        [Required, Display(Name = "Lot No.")]
        public string LotNumber { get; set; }
        [Required, Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required, Display(Name = "Standard Name")]
        public string StockStandardName { get; set; }
        public double LowAmountThreshHold { get; set; }
        [Required, DataType(DataType.Date), Display(Name="Date Entered")]
        public DateTime DateEntered { get; set; }
        [Required, Display(Name = "Solvent Used")]
        public string SolventUsed { get; set; }
        [Required, Display(Name = "Solvent Supplier")]
        public string SolventSupplierName { get; set; }
        [Required]
        public double Purity { get; set; }
        [Display(Name = "Entered By")]
        public string EnteredBy { get; set; }
        [Display(Name = "Last Modified")]
        public DateTime? LastModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }


        //foreign keys
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}