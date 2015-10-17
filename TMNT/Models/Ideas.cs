using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class Ideas {
        [Key]
        public int IdeaId { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Your Comment is Required"), DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
    }
}