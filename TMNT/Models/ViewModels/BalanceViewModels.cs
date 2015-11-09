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
        [Required(ErrorMessage = "Device Code is Required"), Display(Name = "Device Code")]
        public string DeviceCode { get; set; }
        [Required(ErrorMessage = "Department Name is Required"), Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Sub Department is Required"), Display(Name = "Sub Department")]
        public string SubDepartmentName { get; set; }
        [Required(ErrorMessage = "Location Name is Required"), Display(Name = "Location Name")]
        public string LocationName { get; set; }
        [Required(ErrorMessage = "Number of Decimals is Required"), Display(Name = "Number of Decimals")]
        public int NumberOfDecimals { get; set; }
        [Required(ErrorMessage = "First Weight Limit is Required"), Display(Name = "First Weight Limit")]
        public int WeightLimitOne { get; set; }
        [Required(ErrorMessage = "Second Weight Limit is Required"), Display(Name = "Second Weight Limit")]
        public int WeightLimitTwo { get; set; }
        [Required(ErrorMessage = "Third Weight Limit is Required"), Display(Name = "Third Weight Limit")]
        public int WeightLimitThree { get; set; }

        //properties to help populate the view
        public List<string> LocationNames { get; set; }
        public List<Department> Departments { get; set; }
        public List<Department> SubDepartments { get; set; }
        public List<string> WeightUnits { get; set; }
    }

    public class BalanceDetailsViewModel {

    }
}