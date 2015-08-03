﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMNT.Models {
    public class PrepList {
        [Key]
        public int PrepListId { get; set; }
        public virtual ICollection<WorkingStandard> WorkingStandards { get; set; }
        public virtual ICollection<IntermediateStandard> IntermediateStandards { get; set; }
        public virtual ICollection<PrepListItem> PrepListItems { get; set; }
    }
}
