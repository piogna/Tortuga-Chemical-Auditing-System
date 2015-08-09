namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTypeToInventoryItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "Type");
        }
    }
}
