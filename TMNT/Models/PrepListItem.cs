using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class PrepListItem {
        [Key]
        public int PrepListItemId { get; set; }
        [Required]
        public int Amount { get; set; }

        //foreign keys
        public virtual StockReagent StockReagent { get; set; }
        //public double? StockReagentAmountTaken { get; set; }
        public virtual StockStandard StockStandard { get; set; }
        //public double? StockStandardAmountTaken { get; set; }
        public virtual IntermediateStandard IntermediateStandard { get; set; }
        //public double? IntermediateStandardAmountTaken { get; set; }
        //public virtual WorkingStandard WorkingStandard { get; set; }
        [Required]
        public virtual PrepList PrepList { get; set; }
    }
}