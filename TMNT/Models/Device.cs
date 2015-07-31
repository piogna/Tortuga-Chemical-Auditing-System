using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class Device {
        [Key]
        public int DeviceId { get; set; }
        public bool IsVerified { get; set; }
        public DateTime LastVerified { get; set; }
        public string LastVerifiedBy { get; set; }
        public string DeviceType { get; set; }

        [Required]
        public virtual Department Department { get; set; }

        public virtual ICollection<DeviceVerification> DeviceVerifications { get; set; }
    }
}