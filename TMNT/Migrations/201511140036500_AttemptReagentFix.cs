namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttemptReagentFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockReagents", "CatalogueCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockReagents", "CatalogueCode");
        }
    }
}