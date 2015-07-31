using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models.ViewModels {
    public class IntermediateStandardViewModel {

        [Key]
        public int IntermediateStandardId { get; set; }
        public string Replaces { get; set; }
        [Display(Name = "Replaced By")]
        public string ReplacedBy { get; set; }

        //foreign keys
        public virtual PrepList PrepList { get; set; }
        public virtual PrepListItem PrepListItem { get; set; }

        public virtual StockStandard StockStandard { get; set; }

        [Required]
        public int Amount { get; set; }

        //inventory poperties
        [Required, Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [Required, Display(Name = "Inventory Item Name")]
        public string InventoryItemName { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public int Grade { get; set; }
        [Required, Display(Name = "Case Number")]
        public int CaseNumber { get; set; }
        [Required, Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        //[Required]
        //public byte[] MSDS { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
    }
}