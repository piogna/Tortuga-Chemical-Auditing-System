﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models.ViewModels {
    public class StockReagentViewModel {

        //reagent properties
        public int ReagentId { get; set; }
        public string IdCode { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Entered")]
        public DateTime DateEntered { get; set; }
        [Display(Name = "Reagent Name")]
        public string ReagentName { get; set; }
        [Display(Name = "Entered By")]
        public string EnteredBy { get; set; }
        [DataType(DataType.Date), Display(Name = "Date Last Modified")]
        public DateTime? LastModified { get; set; }
        [Display(Name ="Last Modified By")]
        public string LastModifiedBy { get; set; }
        [Display(Name ="Low Amount Threshold")]
        public double LowAmountThreshHold { get; set; }


        //standard poperties
        [Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        [Display(Name = "Inventory Item Name")]
        public string InventoryItemName { get; set; }
        public int Amount { get; set; }
        public int Grade { get; set; }
        [Display(Name = "Case Number")]
        public int CaseNumber { get; set; }
        [Display(Name = "Used For"), DataType(DataType.MultilineText)]
        public string UsedFor { get; set; }
        [Display(Name="C of A")]
        public CertificateOfAnalysis CertificateOfAnalysis { get; set; }
        public MSDS MSDS { get; set; }
        public Unit Unit { get; set; }
    }
}