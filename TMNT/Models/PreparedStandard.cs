using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class PreparedStandard {
        public PreparedStandard() {
            InventoryItems = new List<InventoryItem>();
        }
        [Key]
        public int PreparedStandardId { get; set; }
        public string MaxxamId { get; set; }
        public string IdCode { get; set; }
        public string PreparedStandardName { get; set; }
        public string SolventUsed { get; set; }
        public string SolventSupplierName { get; set; }
        public double Purity { get; set; }
        public string LastModifiedBy { get; set; }

        //shared

        [DataType(DataType.Date)]
        public DateTime DateReceived { get; set; }
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
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}