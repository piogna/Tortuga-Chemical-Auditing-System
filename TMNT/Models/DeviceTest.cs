using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class DeviceTest {
        [Key]
        public int DeviceTestId { get; set; }
        [Required]
        public virtual DeviceVerification DeviceVerification { get; set; }
    }
}