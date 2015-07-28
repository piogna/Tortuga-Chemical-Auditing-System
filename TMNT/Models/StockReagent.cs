﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TMNT.Models {
    public class StockReagent {
        public StockReagent() {
            InventoryItems = new List<InventoryItem>();
        }

        [Key]
        public int ReagentId { get; set; }
        [Required, Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Entered")]
        public DateTime DateEntered { get; set; }
        [Required, Display(Name = "Reagent Name")]
        public string ReagentName { get; set; }
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
