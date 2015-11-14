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
        public string UsedFor { get; set; }
        public string Type { get; set; }
        public string SupplierName { get; set; }
        public string StorageRequirements { get; set; }
        public int NumberOfBottles { get; set; }
        public string InitialAmount { get; set; }
        public string OtherUnitExplained { get; set; }
        public string ConcentrationOtherUnitExplained { get; set; }

        public virtual ICollection<SpikingStandard> SpikingStandards { get; set; }
        public virtual ICollection<SurrogateSpikingStandard> SurrogateSpikingStandards { get; set; }
        public virtual ICollection<CertificateOfAnalysis> CertificatesOfAnalysis { get; set; }
        public virtual ICollection<MSDS> MSDS { get; set; }
        //foreign keys
        public virtual Department Department { get; set; }
        public virtual StockStandard StockStandard { get; set; }
        public virtual StockReagent StockReagent { get; set; }
        public virtual PreparedReagent PreparedReagent { get; set; }
        public virtual PreparedStandard PreparedStandard { get; set; }
        public virtual IntermediateStandard IntermediateStandard { get; set; }
        public virtual WorkingStandard WorkingStandard { get; set; }
        public virtual Device FirstDeviceUsed { get; set; }
        public virtual Device SecondDeviceUsed { get; set; }
    }
}