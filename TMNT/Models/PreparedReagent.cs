﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class PreparedReagent {
        public PreparedReagent() {
            InventoryItems = new List<InventoryItem>();
        }
        [Key]
        public int PreparedReagentId { get; set; }
        public string MaxxamId { get; set; }
        public string IdCode { get; set; }
        public string PreparedReagentName { get; set; }
        public string LastModifiedBy { get; set; }
        public string Grade { get; set; }
        public string GradeAdditionalNotes { get; set; }

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