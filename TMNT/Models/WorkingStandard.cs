using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class WorkingStandard {
        [Key]
        public int WorkingStandardId { get; set; }
        [Required, Display(Name="Unique ID Code")]
        public string IdCode { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Preparation Date")]
        public DateTime PreparationDate { get; set; }
        [Required]
        public int Source { get; set; }
        [Required]
        public double Grade { get; set; }

        //foreign keys
        [Required]
        public virtual PrepList PrepList { get; set; }
    }
}