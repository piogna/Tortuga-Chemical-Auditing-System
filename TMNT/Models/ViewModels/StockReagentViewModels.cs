using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class StockReagentIndexViewModel {
        public int ReagentId { get; set; }
        [Display(Name = "Lot #")]
        public string LotNumber { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Reagent Name")]
        public string ReagentName { get; set; }
        [Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        [Display(Name = "Date Opened"), DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsExpiring { get; set; }
    }
    public class StockReagentDetailsViewModel {
        //reagent properties
        public int ReagentId { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date Opened"), DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Reagent Name")]
        public string ReagentName { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Display(Name = "Lot #")]
        public string LotNumber { get; set; }
        [Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsExpiring { get; set; }
        public bool IsOpened { get; set; }

        //inventory poperties
        [Display(Name = "Supplier")]
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
        [Display(Name = "Grade Additional Notes")]
        public string GradeAdditionalNotes { get; set; }
        [Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Display(Name = "Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }
        [Display(Name = "Number of Bottles")]
        public int NumberOfBottles { get; set; }

        public List<PrepListItem> PrepListItems { get; set; }
        public List<CertificateOfAnalysis> AllCertificatesOfAnalysis { get; set; }
        public List<MSDS> AllMSDS { get; set; }
    }

    public class StockReagentCreateViewModel {
        //reagent properties
        public int ReagentId { get; set; }
        //[Display(Name = "ID Code")]
        //public string IdCode { get; set; }
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
        [Display(Name ="Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Required(ErrorMessage = "Lot Number is Required"), Display(Name = "#")]
        public string LotNumber { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Date Received"), DataType(DataType.Date)]
        public DateTime DateReceived { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //inventory poperties
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "Storage Requirements is Required"), Display(Name = "Storage Req's")]
        public string StorageRequirements { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "SDS Notes"), DataType(DataType.MultilineText)]
        public string MSDSNotes { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [Display(Name = "Inventory Item Name")]
        public string InventoryItemName { get; set; }
        public string Grade { get; set; }
        [Display(Name = "Grade Additional Notes")]
        public string GradeAdditionalNotes { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Display(Name="Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }
        [Display(Name = "Number of Bottles")]
        public int NumberOfBottles { get; set; }
        public int InitialAmount { get; set; }
        public string InitialAmountUnits { get; set; }

        public List<PrepListItem> PrepListItems { get; set; }
        public List<CertificateOfAnalysis> AllCertificatesOfAnalysis { get; set; }
        public List<MSDS> AllMSDS { get; set; }

        //View Model data fields
        public List<string> Storage = new List<string>() { "Fridge", "Freezer", "Shelf", "Other" };
        public List<string> ChemicalTypes = new List<string>() { "Reagent", "Standard", "Intermediate Standard" };
        public List<string> Grades = new List<string>() { "A.C.S.", "Reagent", "U.S.P.", "N.F.", "Lab", "Purified", "Technical" };
        public List<Device> BalanceDevices { get; set; }
        public List<Device> VolumetricDevices { get; set; }

        public List<Unit> WeightUnits { get; set; }
        public List<Unit> VolumetricUnits { get; set; }
        public Unit OtherUnit { get; set; }
    }

    public class StockReagentEditViewModel {
        //reagent properties
        public int ReagentId { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Reagent Name")]
        public string ReagentName { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Required(ErrorMessage = "Lot Number is Required"), Display(Name = "Lot #")]
        public string LotNumber { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

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
    }
}