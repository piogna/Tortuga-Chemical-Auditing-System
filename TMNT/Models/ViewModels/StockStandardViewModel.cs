using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class StockStandardViewModel {
        public int StockStandardId { get; set; }

        //stock standard properties
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Standard Name")]
        public string StockStandardName { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Entered")]
        public DateTime DateEntered { get; set; }
        [Display(Name ="Entered By")]
        public string EnteredBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? LastModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Display(Name = "Solvent Used")]
        public string SolventUsed { get; set; }
        //[Required]
        public double Purity { get; set; }
        public double LowAmountThreshHold { get; set; }

        //inventory properties
        [Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [Display(Name = "Amount Remaining")]
        public int Amount { get; set; }
        //[Required]
        public int Grade { get; set; }
        [Display(Name = "Case Number")]
        public int CaseNumber { get; set; }
        [Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        public MSDS MSDS { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }

        public List<CertificateOfAnalysis> AllCertificatesOfAnalysis { get; set; }
        public List<MSDS> AllMSDS { get; set; }
    }
}