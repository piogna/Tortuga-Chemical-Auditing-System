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
        [Required, DataType(DataType.Date), Display(Name = "Discard Date")]
        public DateTime DiscardDate { get; set; }
        public int? Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public int? ReplacedBy { get; set; }

        //foreign keys
        [Required]
        public virtual PrepList PrepList { get; set; }
    }
}