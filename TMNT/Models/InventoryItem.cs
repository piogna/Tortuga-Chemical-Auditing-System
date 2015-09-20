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
        public string CatalogueCode { get; set; }
        public double LowAmountThreshHold { get; set; }
        public string Grade { get; set; }
        public string GradeAdditionalNotes { get; set; }
        public string UsedFor { get; set; }
        public string Type { get; set; }
        public string SupplierName { get; set; }
        public string CreatedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOpened { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateModified { get; set; }
        public string StorageRequirements { get; set; }
        public string DevicesUsed { get; set; }


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
        public virtual MaxxamMadeReagent MaxxamMadeReagent { get; set; }
        public virtual MaxxamMadeStandard MaxxamMadeStandard { get; set; }
        public virtual IntermediateStandard IntermediateStandard { get; set; }
    }
}