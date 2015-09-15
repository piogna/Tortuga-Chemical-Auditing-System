namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryItems", "CatalogueCode", c => c.String());
            AlterColumn("dbo.InventoryItems", "UsedFor", c => c.String());
            AlterColumn("dbo.InventoryItems", "CreatedBy", c => c.String());
            AlterColumn("dbo.InventoryItems", "StorageRequirements", c => c.String());
            AlterColumn("dbo.StockReagents", "LotNumber", c => c.String());
            AlterColumn("dbo.StockReagents", "IdCode", c => c.String());
            AlterColumn("dbo.StockReagents", "ReagentName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StockReagents", "ReagentName", c => c.String(nullable: false));
            AlterColumn("dbo.StockReagents", "IdCode", c => c.String(nullable: false));
            AlterColumn("dbo.StockReagents", "LotNumber", c => c.String(nullable: false));
            AlterColumn("dbo.InventoryItems", "StorageRequirements", c => c.String(nullable: false));
            AlterColumn("dbo.InventoryItems", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("dbo.InventoryItems", "UsedFor", c => c.String(nullable: false));
            AlterColumn("dbo.InventoryItems", "CatalogueCode", c => c.String(nullable: false));
        }
    }
}
