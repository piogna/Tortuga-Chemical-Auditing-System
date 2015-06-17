using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class DeviceTest {
        [Key]
        public int DeviceTestId { get; set; }
        [Required]
        public virtual DeviceVerification DeviceVerification { get; set; }
    }
}