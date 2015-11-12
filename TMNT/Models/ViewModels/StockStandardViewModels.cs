using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class StockStandardIndexViewModel {
        public int StockStandardId { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Standard Name")]
        public string StockStandardName { get; set; }
        [Display(Name = "Date Opened"), DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Lot #")]
        public string LotNumber { get; set; }
        [Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsExpiring { get; set; }
    }

    public class StockStandardCreateViewModel {
        public int StockStandardId { get; set; }
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
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "{0} is Required")]
        public string Purity { get; set; }
        //public double LowAmountThreshHold { get; set; }
        [Required(ErrorMessage = "Lot Number is Required"), Display(Name = "Lot #")]
        public string LotNumber { get; set; }
        [Display(Name = "Expiry Date"), DataType(DataType.Date)]//Required(ErrorMessage = "{0} is Required"), 
        public DateTime? ExpiryDate { get; set; }
        [Display(Name = "Days Until Expired")]
        public int? DaysUntilExpired { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Date Received"), DataType(DataType.Date)]
        public DateTime DateReceived { get; set; }

        public bool IsExpiryDateBasedOnDays { get; set; }

        //inventory properties
        public List<Device> DevicesUsed { get; set; }
        public Device DeviceOne { get; set; }
        public Device DeviceTwo { get; set; }
        [Required(ErrorMessage = "Storage Requirements is Required"), Display(Name = "Storage Req's")]
        public string StorageRequirements { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "SDS Notes"), DataType(DataType.MultilineText)]
        public string MSDSNotes { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }
        [Display(Name = "Number of Bottles")]
        public int NumberOfBottles { get; set; }
        public int InitialAmount { get; set; }
        public string InitialAmountUnits { get; set; }
        public string InitialConcentrationUnits { get; set; }
        public int Concentration { get; set; }
        [Display(Name = "Other Unit Follow-up")]
        public string OtherUnitExplained { get; set; }
        [Display(Name = "Other Unit Follow-up")]
        public string ConcentrationOtherUnitExplained { get; set; }

        public List<CertificateOfAnalysis> AllCertificatesOfAnalysis { get; set; }
        public List<MSDS> AllMSDS { get; set; }

        //View Model data fields
        public List<string> Storage = new List<string>() { "Fridge", "Freezer", "Shelf", "Other" };
        public IEnumerable<Device> BalanceDevices { get; set; }
        public IEnumerable<Device> VolumetricDevices { get; set;}

        public List<string> ConcentrationUnits = new List<string>() { "mg/L", "ug/L", "ug/mL", "mg/mL", "ng/L" };
        public List<Unit> WeightUnits { get; set; }
        public List<Unit> VolumetricUnits { get; set; }
        public Unit OtherUnit { get; set; }
    }

    public class StockStandardDetailsViewModel {
        public int StockStandardId { get; set; }

        //stock standard properties
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Standard Name")]
        public string StockStandardName { get; set; }
        [Display(Name = "Date Opened"), DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Date Received"), DataType(DataType.Date)]
        public DateTime DateReceived { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Display(Name = "Solvent Used"), DataType(DataType.MultilineText)]
        public string SolventUsed { get; set; }
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        public string Purity { get; set; }
        [Display(Name = "Lot #")]
        public string LotNumber { get; set; }
        [Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        [Display(Name = "Number of Bottles")]
        public int NumberOfBottles { get; set; }
        public string Concentration { get; set; }
        [Display(Name = "Amount Purchased")]
        public string InitialAmount { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsExpiring { get; set; }
        public bool IsOpened { get; set; }

        //inventory properties
        public List<Device> DevicesUsed { get; set; }
        [Display(Name = "Storage Req's")]
        public string StorageRequirements { get; set; }
        [Display(Name = "SDS Notes"), DataType(DataType.MultilineText)]
        public string MSDSNotes { get; set; }
        [Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
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
        [Display(Name = "Other Unit Follow-up")]
        public string OtherUnitExplained { get; set; }
        [Display(Name = "Other Unit Follow-up")]
        public string ConcentrationOtherUnitExplained { get; set; }

        public List<CertificateOfAnalysis> AllCertificatesOfAnalysis { get; set; }
        public List<MSDS> AllMSDS { get; set; }

        //View Model data fields
        public List<string> Storage { get; set; }
        public IEnumerable<Device> BalanceDevices { get; set; }
        public IEnumerable<Device> VolumeDevices { get; set; }
    }

    public class StockStandardEditViewModel {
        public int StockStandardId { get; set; }

        //stock standard properties
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Standard Name")]
        public string StockStandardName { get; set; }
        [Display(Name = "Date Opened")]
        public DateTime? DateOpened { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        [Required(ErrorMessage = "Lot Number is Required"), Display(Name = "Lot #")]
        public string LotNumber { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //inventory properties
        public List<Device> DevicesUsed { get; set; }
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Certificate of Analysis")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        public Unit Unit { get; set; }
        public Department Department { get; set; }

        public List<CertificateOfAnalysis> AllCertificatesOfAnalysis { get; set; }
        public List<MSDS> AllMSDS { get; set; }

        //View Model data fields
        public List<string> Storage { get; set; }
        public IEnumerable<Device> BalanceDevices { get; set; }
        public IEnumerable<Device> VolumeDevices { get; set; }
    }
}