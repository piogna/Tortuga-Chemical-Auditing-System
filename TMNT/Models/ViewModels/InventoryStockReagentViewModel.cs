using System;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class InventoryStockReagentViewModel {

        //reagent properties
        [Required]
        public string IdCode { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Entered")]
        public DateTime DateEntered { get; set; }
        [Required, Display(Name = "Reagent Name")]
        public string ReagentName { get; set; }
        [Display(Name = "Entered By")]
        public string EnteredBy { get; set; }

        //standard poperties
        [Required, Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [Required, Display(Name = "Inventory Item Name")]
        public string InventoryItemName { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int Grade { get; set; }
        [Required, Display(Name = "Case Number")]
        public int CaseNumber { get; set; }
        [Required, Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Display(Name="C of A")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        public MSDS MSDS { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
        public Unit Unit { get; set; }
    }
}