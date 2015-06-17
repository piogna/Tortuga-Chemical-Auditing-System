using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class SurrogateSpikingStandard {

        [Key]
        public int SurrogateSpikingId { get; set; }

        public string Replaces { get; set; }

        [Required, Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Required, DataType(DataType.Date), Display(Name = "Discard Date")]
        public DateTime DiscardDate { get; set; }

        [Required, Display(Name="Spike Volume")]
        public double SpikeVolume { get; set; }

        [Required, Display(Name="Syringe ID")]
        public virtual Device SyringeId { get; set; }
    }
}