﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class StockReagent {
        public StockReagent() {
            InventoryItems = new List<InventoryItem>();
        }

        [Key]
        public int ReagentId { get; set; }
        [Required, Display(Name = "Lot No.")]
        public string LotNumber { get; set; }
        [Required, Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required, Display(Name = "Reagent Name")]
        public string ReagentName { get; set; }
        public double LowAmountThreshHold { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }

        //foreign keys
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
