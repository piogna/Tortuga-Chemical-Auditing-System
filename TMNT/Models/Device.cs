using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class Device {
        [Key]
        public int DeviceId { get; set; }
        public string DeviceCode { get; set; }
        public bool IsVerified { get; set; }
        public string DeviceType { get; set; }
        public string Status { get; set; }//in good standing, getting repaired etc
        public int NumberOfDecimals { get; set; }
        [Required]
        public string AmountLimitOne { get; set; }
        [Required]
        public string AmountLimitTwo { get; set; }
        [Required]
        public string AmountLimitThree { get; set; }
        public bool IsArchived { get; set; }

        [Required]
        public virtual Department Department { get; set; }

        public virtual ICollection<DeviceVerification> DeviceVerifications { get; set; }
    }
}