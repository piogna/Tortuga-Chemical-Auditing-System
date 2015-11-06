using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class PreparedStandardViewModel {
        public int PreparedStandardId { get; set; }

        //stock standard properties
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Standard Name")]
        public string PreparedStandardName { get; set; }
        [Display(Name = "Date Opened")]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Display(Name = "Solvent Used"), DataType(DataType.MultilineText)]
        public string SolventUsed { get; set; }
        [Required, Display(Name = "Solvent Supplier")]
        public string SolventSupplierName { get; set; }
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        //[Required]
        public double Purity { get; set; }
        //public double LowAmountThreshHold { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }
        [Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //inventory properties
        [Required, Display(Name = "Storage Req's")]
        public string StorageRequirements { get; set; }
        [Display(Name = "SDS Notes"), DataType(DataType.MultilineText)]
        public string MSDSNotes { get; set; }
        [Display(Name = "SDS Expiry Date"), DataType(DataType.Date)]
        public DateTime MSDSExpiryDate { get; set; }
        [Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        //[Display(Name = "Amount Remaining")]
        //public int Amount { get; set; }
        [Display(Name = "Case Number")]
        public int CaseNumber { get; set; }
        [Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }
        public List<PrepListItem> PrepListItems { get; set; }
    }
}