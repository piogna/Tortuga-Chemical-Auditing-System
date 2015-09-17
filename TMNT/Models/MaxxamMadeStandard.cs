using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class MaxxamMadeStandard {
        public MaxxamMadeStandard() {
            InventoryItems = new List<InventoryItem>();
        }
        [Key]
        public int MaxxamMadeStandardId { get; set; }
        public string MaxxamId { get; set; }
        public string IdCode { get; set; }
        public string MaxxamMadeStandardName { get; set; }
        public string SolventUsed { get; set; }
        public string SolventSupplierName { get; set; }
        public double Purity { get; set; }
        public string LastModifiedBy { get; set; }

        //foreign keys
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}