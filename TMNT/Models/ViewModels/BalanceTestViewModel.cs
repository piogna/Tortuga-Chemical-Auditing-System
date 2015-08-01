using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models.ViewModels {
    public class BalanceTestViewModel {
        [Required(ErrorMessage="The balance ID is required"), Display(Name="Balance ID")]
        public int BalanceId { get; set; }
        [Required, Display(Name="Device Code")]
        public string DeviceCode { get; set; }
        public Location Location { get; set; }
        [Display(Name = "Weight One")]
        public double WeightOne { get; set; }
        [Display(Name="Weight Two")]
        public double WeightTwo { get; set; }
        [Display(Name = "Weight Three")]
        public double WeightThree { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
    }
}