using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class WorkingStandardIndexViewModel {
        public int WorkingStandardId { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsExpiring { get; set; }
        public bool IsOpened { get; set; }

        //inventory poperties
        [DataType(DataType.Date), Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }
        public Unit Unit { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
    }

    public class WorkingStandardDetailsViewModel {
        public int WorkingStandardId { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [DataType(DataType.Date), Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }
        [Display(Name = "Date Opened")]
        public DateTime? DateOpened { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        public Unit Unit { get; set; }
        [Required, Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }
        [Display(Name = "Initial Amount")]
        public string InitialAmount { get; set; }
        public string Concentration { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsExpiring { get; set; }
        public bool IsOpened { get; set; }

        //foreign keys
        public Department Department { get; set; }
        public PrepList PrepList { get; set; }
        [Display(Name = "Items")]
        public List<PrepListItem> PrepListItems { get; set; }
    }

    public class WorkingStandardCreateViewModel {
        public int WorkingStandardId { get; set; }
        [Display(Name = "Final Concentration")]
        public int FinalConcentration { get; set; }
        [Display(Name = "Final Volume")]
        public int FinalVolume { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }

        //foreign keys
        public PrepList PrepList { get; set; }
        [Display(Name = "Items")]
        public List<PrepListItem> PrepListItems { get; set; }
        public StockStandard StockStandard { get; set; }
        public List<Device> DevicesUsed { get; set; }
        public Device DeviceOne { get; set; }
        public Device DeviceTwo { get; set; }
        public string TotalAmountUnits { get; set; }
        public string FinalConcentrationUnits { get; set; }

        //inventory poperties
        public bool IsExpiryDateBasedOnDays { get; set; }
        [Display(Name = "Storage Req's")]
        public string StorageRequirements { get; set; }
        [Display(Name = "Date Opened")]
        public DateTime? DateOpened { get; set; }
        [DataType(DataType.Date), Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }
        [Display(Name = "Days Until Expired")]
        public int? DaysUntilExpired { get; set; }
        public string InventoryItemName { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        [Required, Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        public Unit Unit { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "{0} is Required"), Display(Name = "Safety Notes"), DataType(DataType.MultilineText)]
        public string SafetyNotes { get; set; }
        [Display(Name = "Other Unit Follow-up")]
        public string OtherUnitExplained { get; set; }
        [Display(Name = "Other Unit Follow-up")]
        public string ConcentrationOtherUnitExplained { get; set; }

        //View Model data fields
        public List<string> Storage = new List<string>() { "Fridge", "Freezer", "Shelf", "Other" };
        public List<string> ChemicalTypes = new List<string>() { "Reagent", "Standard", "Intermediate Standard" };
        public List<InventoryItem> StockReagents { get; set; }
        public List<InventoryItem> StockStandards { get; set; }
        public List<InventoryItem> IntermediateStandards { get; set; }
        public IEnumerable<Device> BalanceDevices { get; set; }
        public IEnumerable<Device> VolumetricDevices { get; set; }
        public List<Unit> WeightUnits { get; set; }
        public List<Unit> VolumetricUnits { get; set; }
        public Unit OtherUnit { get; set; }
        public List<string> ConcentrationUnits = new List<string>() { "mg/L", "µg/L", "µg/mL", "mg/mL", "ng/L" };

        public string[] Types { get; set; }
    }

    public class WorkingStandardPrepListsViewModel {
        public string AmountsWithUnits { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }
        public string IdCode { get; set; }
        public string Type { get; set; }
    }

    public class WorkingStandardEditViewModel {
        public int WorkingStandardId { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }
        [Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        [Display(Name = "Final Concentration")]
        public int FinalConcentration { get; set; }
        [Display(Name = "Final Volume")]
        public int FinalVolume { get; set; }
        [Display(Name = "Total Amount")]
        public int TotalAmount { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
    }

    public class WorkingStandardPrepListItemsViewModel {
        public string[] AmountsWithUnits { get; set; }
        public string[] Amounts { get; set; }
        public string[] LotNumbers { get; set; }
        public string[] Types { get; set; }
        public string[] Units { get; set; }
    }
}