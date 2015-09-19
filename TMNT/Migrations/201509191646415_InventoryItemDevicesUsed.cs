namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InventoryItemDevicesUsed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "DevicesUsed", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "DevicesUsed");
        }
    }
}
