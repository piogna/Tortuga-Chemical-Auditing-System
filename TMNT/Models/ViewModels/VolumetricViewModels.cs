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

    }

    public class VolumetricDetailsViewModel {

    }

    public class VolumetricCreateViewModel {

    }
}