﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class IntermediateStandardIndexViewModel {
        public int IntermediateStandardId { get; set; }
        public string Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //inventory poperties
        [DataType(DataType.Date), Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        public Unit Unit { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
    }

    public class IntermediateStandardDetailsViewModel {
        public int IntermediateStandardId { get; set; }
        public string Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [DataType(DataType.Date), Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
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

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //foreign keys
        public virtual PrepList PrepList { get; set; }
        [Display(Name = "Items")]
        public virtual List<PrepListItem> PrepListItems { get; set; }
    }

    public class IntermediateStandardCreateViewModel {
        public int IntermediateStandardId { get; set; }
        public string Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Final Concentration")]
        public int FinalConcentration { get; set; }
        [Display(Name = "Final Volume")]
        public int FinalVolume { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }

        //foreign keys
        public virtual PrepList PrepList { get; set; }
        [Display(Name = "Items")]
        public virtual List<PrepListItem> PrepListItems { get; set; }

        public virtual StockStandard StockStandard { get; set; }

        //properties to help with views and have nothing to do with the db
        public bool IsExpired { get; set; }
        public bool IsOpened { get; set; }

        //inventory poperties
        [Display(Name = "Storage Req's")]
        public string StorageRequirements { get; set; }
        [Display(Name = "Date Opened")]
        public DateTime? DateOpened { get; set; }
        [DataType(DataType.Date), Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
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
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        //[Required(ErrorMessage = "{0} is Required"), Display(Name = "SDS Notes"), DataType(DataType.MultilineText)]
        //public string MSDSNotes { get; set; }

        //View Model data fields
        public List<string> Storage = new List<string>() { "Fridge", "Freezer", "Shelf" };
        public List<string> ChemicalTypes = new List<string>() { "Reagent", "Standard", "Intermediate Standard" };
        public List<InventoryItem> StockReagents { get; set; }
        public List<InventoryItem> StockStandards { get; set; }
        public List<InventoryItem> IntermediateStandards { get; set; }
        public List<Unit> WeightUnits { get; set; }
        public List<Unit> VolumetricUnits { get; set; }
        public Unit OtherUnit { get; set; }

        public string[] Types { get; set; }
        //public List<IntermediateStandardPrepListsViewModel> PrepListsInTable { get; set; }
    }

    public class IntermediateStandardPrepListsViewModel {
        public string AmountsWithUnits { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }
        public string IdCode { get; set; }
        public string Type { get; set; }
    }

    public class IntermediateStandardEditViewModel {
        public int IntermediateStandardId { get; set; }
        public string Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }
        [Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }
        [Display(Name = "Expiry Date"), DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        [Display(Name = "Final Concentration")]
        public int FinalConcentration { get; set; }
        [Display(Name = "Final Volume")]
        public int FinalVolume { get; set; }
        [Display(Name = "Total Amount")]
        public int TotalAmount { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
    }

    public class IntermediateStandardPrepListItemsViewModel {
        public string[] AmountsWithUnits { get; set; }
        public string[] Amounts { get; set; }
        public string[] LotNumbers { get; set; }
        public string[] Types { get; set; }
        public string[] Units { get; set; }
    }
}