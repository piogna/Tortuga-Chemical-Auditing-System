using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class Device {
        [Key]
        public int DeviceId { get; set; }
        [Display(Name="Device Code")]
        public string DeviceCode { get; set; }
        [Display(Name="Verification Standing")]
        public bool IsVerified { get; set; }
        [Display(Name="Type")]
        public string DeviceType { get; set; }
        public string Status { get; set; }//in good standing, getting repaired etc

        [Required]
        public virtual Department Department { get; set; }

        public virtual ICollection<DeviceVerification> DeviceVerifications { get; set; }
    }
}