using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class ReportDashboardViewModel {
        [Display(Name = "Most Used Standard")]
        public string MostUsedStandardName { get; set; }
        [Display(Name = "Most Used Reagent")]
        public string MostUsedReagentName { get; set; }
        [Display(Name = "Most Used Intermed.")]
        public string MostUsedIntermediateStandardName { get; set; }
        public Department Department { get; set; }
    }

    public class ReportExpiringStockViewModel {
        [Display(Name = "Days Until Expired")]
        public string DaysUntilExpired { get; set; }
        public string Type { get; set; }
        [Display(Name = "Expiry Date")]
        public string ExpiryDate { get; set; }
        //[Display(Name = "Date Opened")]
        //public string DateOpened { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        public string Department { get; set; }
        public bool IsExpired { get; set; }
    }

    public class ReportDeviceVerificationViewModel {
        public string DeviceType { get; set; }
        public string DeviceCode { get; set; }
        public string Department { get; set; }
        public string IsVerified { get; set; }
        public string Status { get; set; }
    }

    public class BalanceApiModel {
        public int BalanceId { get; set; }
        public string DeviceCode { get; set; }
        public Location Location { get; set; }
        public Department Department { get; set; }
        public bool IsVerified { get; set; }
        public string Status { get; set; }
        public int NumberOfDecimals { get; set; }

        public DateTime? LastVerified { get; set; }
        public string LastVerifiedBy { get; set; }//full name

        public IEnumerable<DeviceVerification> DeviceVerifications { get; set; }
    }
}