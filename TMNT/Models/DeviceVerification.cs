using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class DeviceVerification {
        [Key]
        public int DeviceVerificationId { get; set; }

        [Display(Name = "Date Last Verified"), DataType(DataType.Date)]
        public DateTime? VerifiedOn { get; set; }
        public double? WeightOne { get; set; }
        public double? WeightTwo { get; set; }
        public double? WeightThree { get; set; }
        public double? VolumeOne { get; set; }
        public double? VolumeTwo { get; set; }
        public double? VolumeThree { get; set; }
        public bool DidTestPass { get; set; }
        public string WeightId { get; set; }

        public ICollection<DeviceTest> DeviceTests { get; set; }

        //foreign keys
        [Required]
        [Display(Name = "Last Verified By")]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public virtual Device Device { get; set; }
    }
}