﻿using System;
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
        public string Replaces { get; set; }
        public string ReplacedBy { get; set; }
        public string TotalVolume { get; set; }
        public string FinalConcentration { get; set; }
        public string LastModifiedBy { get; set; }
        public string SafetyNotes { get; set; }
        public int FinalVolume { get; set; }

        //foreign keys
        [Required]
        public virtual PrepList PrepList { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}