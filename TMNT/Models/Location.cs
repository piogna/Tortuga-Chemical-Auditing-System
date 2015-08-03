using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class Location {
        [Key]
        public int LocationId { get; set; }
        [Required, Display(Name="Location Code")]
        public string LocationCode { get; set; }
        [Required, Display(Name = "Location Name")]
        public string LocationName { get; set; }
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
        [Required, DataType(DataType.PhoneNumber), Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }
        [Required, DataType(DataType.Url)]
        public string Website { get; set; }

        public virtual ICollection<InventoryLocation> InventoryLocations { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
