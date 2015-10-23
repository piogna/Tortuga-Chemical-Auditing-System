namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateReceivedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "DateReceived", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "DateReceived");
        }
    }
}
