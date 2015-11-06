using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class PreparedReagentViewModel {

        //reagent properties
        public int PreparedReagentId { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date Opened"), DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Reagent Name")]
        public string PreparedReagentName { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }
        [Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //inventory poperties
        [Required, Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        [Display(Name = "Storage Req's")]
        public string StorageRequirements { get; set; }
        [Display(Name = "SDS Notes"), DataType(DataType.MultilineText)]
        public string MSDSNotes { get; set; }
        [Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [Display(Name = "Inventory Item Name")]
        public string InventoryItemName { get; set; }
        public string Grade { get; set; }
        [Display(Name = "Case Number")]
        public int CaseNumber { get; set; }
        [Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Display(Name = "Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }

        public List<PrepListItem> PrepListItems { get; set; }
    }
}