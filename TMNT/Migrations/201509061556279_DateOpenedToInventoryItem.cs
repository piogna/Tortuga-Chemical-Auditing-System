namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateOpenedToInventoryItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "DateOpened", c => c.DateTime());
            DropColumn("dbo.StockStandards", "DateOpened");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockStandards", "DateOpened", c => c.DateTime());
            DropColumn("dbo.InventoryItems", "DateOpened");
        }
    }
}
