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
        [Required(ErrorMessage = "Balance Type is Required"), Display(Name = "Balance Type")]
        public string BalanceType { get; set; }
    }

    public class BalanceVerificationViewModel {
        [Required(ErrorMessage = "Balance ID is Required"), Display(Name="Balance ID")]
        public int BalanceId { get; set; }
        [Required(ErrorMessage = "Device Code is Required"), Display(Name="Device Code")]
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
        [Display(Name = "Last Verified"), DataType(DataType.Date)]
        public DateTime? LastVerified { get; set; }
        public string Status { get; set; }//in good standing, getting repaired etc
        [Display(Name = "Last Verified By")]
        public string LastVerifiedBy { get; set; }//full name
        public Department Department { get; set; }
        [Display(Name = "Last Verified By")]
        public string VerifiedBy { get; set; }
        [Required(ErrorMessage = "Weight ID is Required")]
        public string WeightId { get; set; }
        [Required(ErrorMessage = "Balance Type is Required"), Display(Name = "Balance Type")]
        public string BalanceType { get; set; }

        public int NumberOfTestsToVerify { get; set; }
        public string WeightLimitOne { get; set; }
        public string WeightLimitTwo { get; set; }
        public string WeightLimitThree { get; set; }

        //properties to populate view
        public List<string> LocationNames { get; set; }
        public string CurrentLocation { get; set; }
        public int NumberOfDecimals { get; set; }

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
        [Display(Name = "Third Weight Limit")]
        public int WeightLimitThree { get; set; }
        [Required(ErrorMessage = "Number of Tests is Required"), Display(Name = "Number of Tests")]
        public int NumberOfTestsToVerify { get; set; }
        [Required(ErrorMessage = "Balance Type is Required"), Display(Name = "Balance Type")]
        public string BalanceType { get; set; }

        //properties to help populate the view
        public List<string> LocationNames { get; set; }
        public List<Department> Departments { get; set; }
        public List<Department> SubDepartments { get; set; }
        public List<string> WeightUnits { get; set; }
        public List<string> BalanceTypes = new List<string>() { "Analytical", "Top Loading" };
    }

    public class BalanceDetailsViewModel {
        [Required(ErrorMessage = "Balance ID is Required"), Display(Name = "Balance ID")]
        public int BalanceId { get; set; }
        [Required(ErrorMessage = "Device Code is Required"), Display(Name = "Device Code")]
        public string DeviceCode { get; set; }
        public Location Location { get; set; }
        public Department Department { get; set; }
        [Display(Name = "Verification Standing")]
        public bool IsVerified { get; set; }
        public string Status { get; set; }//in good standing, getting repaired etc
        [Display(Name = "Number of Decimals")]
        public int NumberOfDecimals { get; set; }
        [Required(ErrorMessage = "Balance Type is Required"), Display(Name = "Balance Type")]
        public string BalanceType { get; set; }

        [Display(Name = "Date Last Verified"), DataType(DataType.Date)]
        public DateTime? LastVerified { get; set; }
        [Display(Name = "Last Verified By")]
        public string LastVerifiedBy { get; set; }//full name

        public List<DeviceVerification> DeviceVerifications { get; set; }
    }
}