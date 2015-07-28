namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StockReagents", "LastModified", c => c.DateTime());
            AlterColumn("dbo.StockStandards", "LastModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StockStandards", "LastModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.StockReagents", "LastModified", c => c.DateTime(nullable: false));
        }
    }
}
