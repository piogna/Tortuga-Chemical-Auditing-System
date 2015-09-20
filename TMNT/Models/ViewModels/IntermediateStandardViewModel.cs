using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class IntermediateStandardViewModel {

        public int IntermediateStandardId { get; set; }
        public string Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }
        [Display(Name="ID Code")]
        public string IdCode { get; set; }
        [Display(Name = "Final Concentration")]
        public int FinalConcentration { get; set; }
        [Display(Name = "Final Volume")]
        public int FinalVolume { get; set; }
        [Display(Name = "Maxxam Id")]
        public string MaxxamId { get; set; }

        //foreign keys
        public virtual PrepList PrepList { get; set; }
        [Display(Name="Items")]
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
        [Display(Name = "SDS")]
        public MSDS MSDS { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }
    }
}