using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class BalanceIndexViewModel {
        public int BalanceId { get; set; }
        [Display(Name = "Device Code")]
        public string DeviceCode { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Display(Name = "Verification Standing")]
        public bool IsVerified { get; set; }
        [Display(Name = "Last Verified By")]
        public string LastVerifiedBy { get; set; }//full name
    }

    public class BalanceVerificationViewModel {
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
        [Display(Name = "Date Last Verified"), DataType(DataType.Date)]
        public DateTime? LastVerified { get; set; }
        public string Status { get; set; }//in good standing, getting repaired etc
        [Display(Name = "Last Verified By")]
        public string LastVerifiedBy { get; set; }//full name
        public virtual Department Department { get; set; }
        [Display(Name = "Last Verified By")]
        public virtual ApplicationUser User { get; set; }

        //for details
        public List<DeviceVerification> DeviceVerifications { get; set; }
    }

    public class BalanceCreateViewModel {
        [Display(Name = "Device Code")]
        public string DeviceCode { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Display(Name = "Sub Department")]
        public string SubDepartment { get; set; }
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        //properties to help populate the view
        public List<string> LocationNames { get; set; }
        public List<string> DepartmentNames { get; set; }
        public List<string> SubDepartmentNames { get; set; }
    }

    public class BalanceDetailsViewModel {

    }
}