namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForceUpdateDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrepListItems", "StockReagentAmountTaken", c => c.Double());
            AddColumn("dbo.PrepListItems", "StockStandardAmountTaken", c => c.Double());
            AddColumn("dbo.PrepListItems", "IntermediateStandardAmountTaken", c => c.Double());
            AlterColumn("dbo.InventoryItems", "Type", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryItems", "Type", c => c.String(nullable: false));
            DropColumn("dbo.PrepListItems", "IntermediateStandardAmountTaken");
            DropColumn("dbo.PrepListItems", "StockStandardAmountTaken");
            DropColumn("dbo.PrepListItems", "StockReagentAmountTaken");
        }
    }
}
