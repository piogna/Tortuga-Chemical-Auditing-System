using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models {
    public class Department {
        [Key]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage="Department Code is Required"), Display(Name="Department Code")]
        public string DepartmentCode { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }

        //foreign keys
        public virtual Location Location { get; set; }
    }
}