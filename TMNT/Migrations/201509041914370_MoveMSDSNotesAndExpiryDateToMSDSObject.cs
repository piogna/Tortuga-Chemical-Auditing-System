namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveMSDSNotesAndExpiryDateToMSDSObject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MSDS", "MSDSNotes", c => c.String());
            AddColumn("dbo.MSDS", "MSDSExpiryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.InventoryItems", "MSDSNotes");
            DropColumn("dbo.InventoryItems", "MSDSExpiryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InventoryItems", "MSDSExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.InventoryItems", "MSDSNotes", c => c.String());
            DropColumn("dbo.MSDS", "MSDSExpiryDate");
            DropColumn("dbo.MSDS", "MSDSNotes");
        }
    }
}
