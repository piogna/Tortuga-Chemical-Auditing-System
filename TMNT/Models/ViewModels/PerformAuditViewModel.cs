using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models.ViewModels {
    public class PerformAuditViewModel {
        [Required(ErrorMessage = "The ID Code is Required")]
        public string IdCode { get; set; }
        [Required(ErrorMessage = "The Chemical Type is Required")]
        public string Type { get; set; }
    }
}