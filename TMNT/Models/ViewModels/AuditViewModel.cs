using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMNT.Models.ViewModels {
    public class AuditViewModel {
        //public string WorkingStandardId { get; set; }
        //public string IntermediateStandardId { get; set; }
        //public string StandardId { get; set; }

        /* Working Standard Properties */
        [Required, Display(Name="ID")]
        public int WorkingStandardId { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Preparation Date")]
        public DateTime PreparationDate { get; set; }
        [Required]
        public int Source { get; set; }
        [Required]
        public double Grade { get; set; }

        //foreign keys
        //[Required]
        //public virtual PrepList PrepList { get; set; }

        /* Prep List Properties */
        public int PrepListId { get; set; }
        public ICollection<WorkingStandard> WorkingStandards { get; set; }
        public ICollection<IntermediateStandard> IntermediateStandards { get; set; }
        public ICollection<PrepListItem> PrepListItems { get; set; }

        /* Prep List Item Properties*/
        public int PrepListItemId { get; set; }
        [Required]
        public int Amount { get; set; }

        //foreign keys
        public StockReagent StockReagent { get; set; }
        public StockStandard StockStandard { get; set; }
        public IntermediateStandard IntermediateStandard { get; set; }
        [Required]
        public PrepList PrepList { get; set; }

        /* Stock Standard Properties */
        public int StockStandardId { get; set; }
        [Required, Display(Name = "ID Code")]
        public string IdCode { get; set; }
        [Required, Display(Name = "Name")]
        public string StockStandardName { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Date Entered")]
        public DateTime DateEntered { get; set; }
        [Required, Display(Name = "Solvent Used")]
        public string SolventUsed { get; set; }
        [Required]
        public double Purity { get; set; }
    }
}
