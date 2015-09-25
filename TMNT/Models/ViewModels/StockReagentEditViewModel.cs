using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class StockReagentEditViewModel {

        //reagent properties
        public int ReagentId { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date Opened"), DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Reagent Name")]
        public string ReagentName { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Required(ErrorMessage = "Lot Number is Required"), Display(Name = "Lot No.")]
        public string LotNumber { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //inventory poperties
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        [Display(Name = "Inventory Item Name")]
        public string InventoryItemName { get; set; }
        public string Grade { get; set; }
        [Display(Name = "Grade Additional Notes")]
        public string GradeAdditionalNotes { get; set; }
        [Display(Name = "Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }

        public List<PrepListItem> PrepListItems { get; set; }
        public List<CertificateOfAnalysis> AllCertificatesOfAnalysis { get; set; }
        public List<MSDS> AllMSDS { get; set; }

        //public List<object> ItemsWhereReagentUsed { get; set; }
    }
}