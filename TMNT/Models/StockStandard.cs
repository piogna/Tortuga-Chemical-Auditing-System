using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class StockStandard {
        public StockStandard(){
            InventoryItems = new List<InventoryItem>();
        }
        [Key]
        public int StockStandardId { get; set; }
        [Required, Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required, Display(Name = "Name")]
        public string StockStandardName { get; set; }
        [Required, DataType(DataType.Date), Display(Name="Date Entered")]
        public DateTime DateEntered { get; set; }
        [Required, Display(Name = "Solvent Used")]
        public string SolventUsed { get; set; }
        [Required]
        public double Purity { get; set; }
        public string EnteredBy { get; set; }
        public string LastModifiedBy { get; set; }

        //foreign keys
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}