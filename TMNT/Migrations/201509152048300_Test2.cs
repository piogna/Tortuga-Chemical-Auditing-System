namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryItems", "DateOpened", c => c.DateTime());
            AlterColumn("dbo.InventoryItems", "DateModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryItems", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.InventoryItems", "DateOpened", c => c.DateTime(nullable: false));
        }
    }
}
