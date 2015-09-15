namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNullableDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryItems", "DateOpened", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryItems", "DateOpened", c => c.DateTime());
        }
    }
}
