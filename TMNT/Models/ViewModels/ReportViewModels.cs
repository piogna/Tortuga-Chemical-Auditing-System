﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMNT.Models.ViewModels {
    public class ExpiringStockViewModel {
        [Display(Name = "Days Until Expired")]
        public string DaysUntilExpired { get; set; }
        public string Type { get; set; }
        [Display(Name = "Expiry Date")]
        public string ExpiryDate { get; set; }
        [Display(Name = "Date Opened")]
        public string DateOpened { get; set; }
        [Display(Name = "Supplier")]
        public string SupplierName { get; set; }
        public string Department { get; set; }
    }
}