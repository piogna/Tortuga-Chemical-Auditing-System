using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class IntermediateStandard {
        [Key]
        public int IntermediateStandardId { get; set; }

        public string IdCode { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public string Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }
        public int Amount { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        //foreign keys
        [Required]
        public virtual PrepList PrepList { get; set; }
    }
}