namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingMoreUselessFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Locations", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropIndex("dbo.Locations", new[] { "InventoryItem_InventoryItemId" });
            DropColumn("dbo.Locations", "Website");
            DropColumn("dbo.Locations", "InventoryItem_InventoryItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Locations", "InventoryItem_InventoryItemId", c => c.Int());
            AddColumn("dbo.Locations", "Website", c => c.String());
            CreateIndex("dbo.Locations", "InventoryItem_InventoryItemId");
            AddForeignKey("dbo.Locations", "InventoryItem_InventoryItemId", "dbo.InventoryItems", "InventoryItemId");
        }
    }
}
