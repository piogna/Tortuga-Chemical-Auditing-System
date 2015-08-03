using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class Supplier {
        [Key]
        public int SupplierId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required, DataType(DataType.PostalCode), Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required, DataType(DataType.PhoneNumber), Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required, DataType(DataType.PhoneNumber), Display(Name="Fax Number")]
        public string FaxNumber { get; set; }
        [DataType(DataType.Url)]
        public string Website { get; set; }
        [Display(Name = "Contact First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Contact Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Contact Phone Number")]
        public string ContactPhoneNumber { get; set; }
        [Display(Name = "Contact Cell Number")]
        public string CellNumber { get; set; }
        [Display(Name = "Contact Extension")]
        public string Extension { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
    }
}
