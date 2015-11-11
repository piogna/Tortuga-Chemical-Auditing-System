using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class Location {
        [Key]
        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
