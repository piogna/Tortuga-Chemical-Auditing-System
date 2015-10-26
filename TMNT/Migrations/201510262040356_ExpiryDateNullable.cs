namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpiryDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryItems", "ExpiryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryItems", "ExpiryDate", c => c.DateTime(nullable: false));
        }
    }
}
