using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models.ViewModels {
    public class InventoryStockStandardViewModel {
        //[Key]
        //public int StockStandardId { get; set; }

        //stock standard properties
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Name")]
        public string StockStandardName { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Entered")]
        public DateTime DateEntered { get; set; }
        [Display(Name = "Solvent Used")]
        public string SolventUsed { get; set; }
        //[Required]
        public double Purity { get; set; }

        //inventory properties
        [Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        //[Required]
        public int Amount { get; set; }
        //[Required]
        public int Grade { get; set; }
        [Display(Name = "Case Number")]
        public int CaseNumber { get; set; }
        [Display(Name = "Used For")]
        public string UsedFor { get; set; }
        public MSDS MSDS { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        public Unit Unit { get; set; }
    }
}