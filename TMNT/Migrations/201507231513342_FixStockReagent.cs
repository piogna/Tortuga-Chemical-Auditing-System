namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixStockReagent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StockReagents", "EnteredBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StockReagents", "EnteredBy", c => c.String(nullable: false));
        }
    }
}
