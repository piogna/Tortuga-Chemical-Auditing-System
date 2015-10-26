namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DaysUntilExpiredField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "DaysUntilExpired", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "DaysUntilExpired");
        }
    }
}
