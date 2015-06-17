using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMNT.Models.FakeModels {
    public class FakeLowStockStandards : FakeInventoryItem {
        public int AmountRemaining { get; set; }
        public string UnitType { get; set; }

    }
}