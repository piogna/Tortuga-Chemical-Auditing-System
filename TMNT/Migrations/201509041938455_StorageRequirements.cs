namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StorageRequirements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "StorageRequirements", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "StorageRequirements");
        }
    }
}
