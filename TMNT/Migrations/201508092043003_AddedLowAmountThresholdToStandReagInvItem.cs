namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLowAmountThresholdToStandReagInvItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "LowAmountThreshHold", c => c.Double(nullable: false));
            AddColumn("dbo.StockReagents", "LowAmountThreshHold", c => c.Double(nullable: false));
            AddColumn("dbo.StockStandards", "LowAmountThreshHold", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockStandards", "LowAmountThreshHold");
            DropColumn("dbo.StockReagents", "LowAmountThreshHold");
            DropColumn("dbo.InventoryItems", "LowAmountThreshHold");
        }
    }
}
