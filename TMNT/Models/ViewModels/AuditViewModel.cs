using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class AuditViewModel {

        public AuditViewModel()
        {
            Parents = new List<AuditViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IdCode { get; set; }
        public string MaxxamId { get; set; }
        public string ChemType { get; set; }
        public List<AuditViewModel> Parents { get; set; }
    }
}
