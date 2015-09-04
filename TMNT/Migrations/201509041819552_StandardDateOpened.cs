namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StandardDateOpened : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockStandards", "DateOpened", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockStandards", "DateOpened");
        }
    }
}
