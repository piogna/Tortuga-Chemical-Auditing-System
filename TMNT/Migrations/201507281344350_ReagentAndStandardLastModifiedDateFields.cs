namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReagentAndStandardLastModifiedDateFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockReagents", "LastModified", c => c.DateTime(nullable: true));
            AddColumn("dbo.StockStandards", "LastModified", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockStandards", "LastModified");
            DropColumn("dbo.StockReagents", "LastModified");
        }
    }
}
