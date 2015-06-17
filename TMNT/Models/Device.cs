using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class Device {
        [Key]
        public int DeviceId { get; set; }
        [Required]
        public virtual Department Department { get; set; }

        public virtual ICollection<DeviceVerification> DeviceVerifications { get; set; }
    }
}