using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class ReportExpiringStockViewModel {
        [Display(Name = "Days Until Expired")]
        public string DaysUntilExpired { get; set; }
        public string Type { get; set; }
        [Display(Name = "Expiry Date")]
        public string ExpiryDate { get; set; }
        [Display(Name = "Date Opened")]
        public string DateOpened { get; set; }
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        public string Department { get; set; }
    }

    public class ReportDeviceVerificationViewModel {
        public string DeviceType { get; set; }
        public string DeviceCode { get; set; }
        public string Department { get; set; }
        public string IsVerified { get; set; }
        public string Status { get; set; }
    }
}