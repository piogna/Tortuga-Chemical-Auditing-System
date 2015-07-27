namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnteredByAndLastModifiedBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockReagents", "LastModifiedBy", c => c.String());
            AddColumn("dbo.StockStandards", "EnteredBy", c => c.String());
            AddColumn("dbo.StockStandards", "LastModifiedBy", c => c.String());
            AddColumn("dbo.IntermediateStandards", "CreatedBy", c => c.String());
            AddColumn("dbo.IntermediateStandards", "LastModifiedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IntermediateStandards", "LastModifiedBy");
            DropColumn("dbo.IntermediateStandards", "CreatedBy");
            DropColumn("dbo.StockStandards", "LastModifiedBy");
            DropColumn("dbo.StockStandards", "EnteredBy");
            DropColumn("dbo.StockReagents", "LastModifiedBy");
        }
    }
}
