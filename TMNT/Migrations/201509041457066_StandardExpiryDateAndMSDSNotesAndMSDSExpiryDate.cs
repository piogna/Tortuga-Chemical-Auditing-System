namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StandardExpiryDateAndMSDSNotesAndMSDSExpiryDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "MSDSNotes", c => c.String());
            AddColumn("dbo.InventoryItems", "MSDSExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.StockStandards", "ExpiryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockStandards", "ExpiryDate");
            DropColumn("dbo.InventoryItems", "MSDSExpiryDate");
            DropColumn("dbo.InventoryItems", "MSDSNotes");
        }
    }
}
