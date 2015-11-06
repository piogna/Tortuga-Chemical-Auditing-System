using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class Device {
        [Key]
        public int DeviceId { get; set; }
        [Display(Name = "Device Code")]
        public string DeviceCode { get; set; }
        [Display(Name="Verification Standing")]
        public bool IsVerified { get; set; }
        [Display(Name="Type")]
        public string DeviceType { get; set; }
        public string Status { get; set; }//in good standing, getting repaired etc
        public int NumberOfDecimals { get; set; }
        public int AmountLimitOne { get; set; }
        public int AmountLimitTwo { get; set; }
        public int AmountLimitThree { get; set; }

        [Required]
        public virtual Department Department { get; set; }

        public virtual ICollection<DeviceVerification> DeviceVerifications { get; set; }
    }
}