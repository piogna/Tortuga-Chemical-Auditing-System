using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models.ViewModels {
    public class WorkingStandardViewModel {

        [Required, DataType(DataType.Date), Display(Name = "Preparation Date")]
        public DateTime PreparationDate { get; set; }
        [Required]
        public int Source { get; set; }
        [Required]
        public double Grade { get; set; }

        [Required, Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [Required, Display(Name = "Inventory Item Name")]
        public string InventoryItemName { get; set; }
        [Required]
        public int Size { get; set; }
        [Required, Display(Name = "Case Number")]
        public int CaseNumber { get; set; }
        [Required, Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Required, Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
    }
}