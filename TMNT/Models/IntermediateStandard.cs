using System;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class IntermediateStandard {
        [Key]
        public int IntermediateStandardId { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        public string Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }
        public int Amount { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        public virtual Unit Unit { get; set; }
        //foreign keys
        [Required]
        public virtual PrepList PrepList { get; set; }
    }
}