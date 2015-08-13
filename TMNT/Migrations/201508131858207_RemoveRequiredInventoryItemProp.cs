namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredInventoryItemProp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryItems", "Type", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryItems", "Type", c => c.String(nullable: false));
        }
    }
}
