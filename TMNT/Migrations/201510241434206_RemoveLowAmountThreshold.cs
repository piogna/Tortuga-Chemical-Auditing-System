namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLowAmountThreshold : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InventoryItems", "LowAmountThreshHold");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InventoryItems", "LowAmountThreshHold", c => c.Double(nullable: false));
        }
    }
}
