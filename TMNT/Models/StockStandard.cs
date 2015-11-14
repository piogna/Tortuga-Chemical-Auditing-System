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
        public string LotNumber { get; set; }
        public string IdCode { get; set; }
        public string StockStandardName { get; set; }
        public string SolventUsed { get; set; }
        public string Purity { get; set; }
        public string LastModifiedBy { get; set; }
        public string Concentration { get; set; }
        public string CatalogueCode { get; set; }

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