using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class UserSettings {
        [Key]
        public int UserSettingsId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Language { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}