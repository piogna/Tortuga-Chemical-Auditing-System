namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveExpiryDateFromStandardToInventoryItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "ExpiryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.StockStandards", "ExpiryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockStandards", "ExpiryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.InventoryItems", "ExpiryDate");
        }
    }
}
