using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMNT.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int ExpiringItemsCount { get; set; }
        public IEnumerable<InventoryItem> ExpiredItems { get; set; }
        public int CertificatesCount { get; set; }
        public int PendingVerificationCount { get; set; }
        public string Department { get; set; }
        public string LocationName { get; set; }
        public string  Role { get; set; }
        public string Name { get; set; }
    }
}