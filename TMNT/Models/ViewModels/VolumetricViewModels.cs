using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class VolumetricIndexViewModel {

        [Display(Name = "Volumetric ID")]
        public int VolumetricId { get; set; }
        [Display(Name = "Device Code")]
        public string DeviceCode { get; set; }
        public Department Department { get; set; }
        [Display(Name = "Balance Used")]
        public Device BalanceUsed { get; set; }
        [Display(Name = "Is Verified")]
        public bool IsVerified { get; set; }
        [DataType(DataType.Date), Display(Name = "Last Verified")]
        public DateTime LastVerified { get; set; }
        [Display(Name = "Last Verified By")]
        public string LastVerifiedBy { get; set; }
        
        public List<DeviceVerification> DeviceVerifications { get; set; }
    }

    public class VolumetricVerificationViewModel {
        [Required(ErrorMessage = "Balance ID is Required"), Display(Name = "Balance ID")]
        public int VolumetricId { get; set; }
        [Required(ErrorMessage = "Device Code is Required"), Display(Name = "Device Code")]
        public string DeviceCode { get; set; }
        public Location Location { get; set; }
        [Display(Name = "Weight One")]
        public double? WeightOne { get; set; }
        [Display(Name = "Weight Two")]
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

    public class VolumetricDetailsViewModel {

    }

    public class VolumetricCreateViewModel {
        public int VolumetricDeviceId { get; set; }
        public string DeviceCode { get; set; }
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Sub Department is Required"), Display(Name = "Sub Department")]
        public string SubDepartmentName { get; set; }
        [Required(ErrorMessage = "Location Name is Required"), Display(Name = "Location Name")]
        public string LocationName { get; set; }
        public string VolumetricType { get; set; }
        public string Categorization { get; set; }
        public string Frequency { get; set; }
        public string Volume { get; set; }
        public string AcceptanceCriteria { get; set; }

        //properties to help populate the view
        public List<string> LocationNames { get; set; }
        public List<Department> Departments { get; set; }
        public List<Department> SubDepartments { get; set; }
        public List<string> DeviceTypes = new List<string>() { "Pipette - Adjustable", "Pipette - Fixed", "Dispenser - Adjustable", "Pipette - Fixed", "Syringe", "Class A Labware", "Class B Labware", "Tubes", "Scoops" };
        public List<string> Categorizations = new List<string>() { "Critical", "Non-Critical" };
        public List<string> Frequencies = new List<string>() { "Daily", "Weekly", "Monthly", "Annually" };
        public List<string> AcceptanceCriterias = new List<string>() { "2%", "5%", "10%" };
    }
}