using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class StockStandardViewModel {
        public int StockStandardId { get; set; }

        //stock standard properties
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Standard Name")]
        public string StockStandardName { get; set; }
        [Display(Name = "Date Opened")]
        public DateTime? DateOpened { get; set; }
        [Display(Name ="Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Solvent Used"), DataType(DataType.MultilineText)]
        public string SolventUsed { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Solvent Supplier")]
        public string SolventSupplierName { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public double Purity { get; set; }
        //public double LowAmountThreshHold { get; set; }
        [Required(ErrorMessage = "Lot Number is Required"), Display(Name = "Lot No.")]
        public string LotNumber { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //inventory properties
        [Required(ErrorMessage = "Storage Requirements is Required"), Display(Name = "Storage Req's")]
        public string StorageRequirements { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "SDS Notes"), DataType(DataType.MultilineText)]
        public string MSDSNotes { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }

        public List<CertificateOfAnalysis> AllCertificatesOfAnalysis { get; set; }
        public List<MSDS> AllMSDS { get; set; }
    }
}