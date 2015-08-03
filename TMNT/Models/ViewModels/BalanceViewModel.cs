using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class BalanceViewModel {
        [Required(ErrorMessage="The balance ID is required"), Display(Name="Balance ID")]
        public int BalanceId { get; set; }
        [Required, Display(Name="Device Code")]
        public string DeviceCode { get; set; }
        public Location Location { get; set; }
        [Display(Name="Level/Clean")]
        public List<bool> LevelClean { get; set; }
        [Display(Name = "Weight One")]
        public double? WeightOne { get; set; }
        [Display(Name="Weight Two")]
        public double? WeightTwo { get; set; }
        [Display(Name = "Weight Three")]
        public double? WeightThree { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
        [Display(Name = "Verification Standing")]
        public bool IsVerified { get; set; }
        [Display(Name = "Date Last Verified")]
        public DateTime? LastVerified { get; set; }
        public string Status { get; set; }//in good standing, getting repaired etc
        public virtual Department Department { get; set; }
        [Display(Name = "Last Verified By")]
        public virtual ApplicationUser User { get; set; }
    }
}