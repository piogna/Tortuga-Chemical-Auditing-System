using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class DeviceVerification {
        [Key]
        public int DeviceVerificationId { get; set; }

        public ICollection<DeviceTest> DeviceTests { get; set; }

        //foreign keys
        [Required]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public virtual Device Device { get; set; }
    }
}