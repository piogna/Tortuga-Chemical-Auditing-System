using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class InventoryItem {
        public InventoryItem() {
            CertificatesOfAnalysis = new List<CertificateOfAnalysis>();
            MSDS = new List<MSDS>();
        }

        [Key]
        public int InventoryItemId { get; set; }
        [Required, Display(Name = "Catalogue Code")]
        public string CatalogueCode { get; set; }
        public double LowAmountThreshHold { get; set; }
        public string Grade { get; set; }
        [Required, Display(Name = "Case Number")]
        public int CaseNumber { get; set; }
        [Required, Display(Name = "Used For")]
        public string UsedFor { get; set; }
        public string Type { get; set; }
        public string SupplierName { get; set; }
        [Required, Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Date Opened")]
        public DateTime? DateOpened { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Modified")]
        public DateTime DateModified { get; set; }
        [Required, Display(Name = "Storage Req's")]
        public string StorageRequirements { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        public virtual ICollection<SpikingStandard> SpikingStandards { get; set; }
        public virtual ICollection<SurrogateSpikingStandard> SurrogateSpikingStandards { get; set; }
        public virtual ICollection<CertificateOfAnalysis> CertificatesOfAnalysis { get; set; }
        public virtual ICollection<MSDS> MSDS { get; set; }
        //foreign keys
        //Supplier is not in use at this time as we're not sure if all their information is required. an input field may suffice and is what's in use for now
        public virtual Supplier Supplier { get; set; }
        public virtual Department Department { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual StockStandard StockStandard { get; set; }
        public virtual StockReagent StockReagent { get; set; }
        public virtual IntermediateStandard IntermediateStandard { get; set; }
    }
}