namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StockReagents", "EnteredBy", c => c.String());
            AlterColumn("dbo.IntermediateStandards", "Replaces", c => c.String());
            AlterColumn("dbo.IntermediateStandards", "ReplacedBy", c => c.String());
            DropColumn("dbo.IntermediateStandards", "DiscardDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IntermediateStandards", "DiscardDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IntermediateStandards", "ReplacedBy", c => c.Int());
            AlterColumn("dbo.IntermediateStandards", "Replaces", c => c.Int());
            AlterColumn("dbo.StockReagents", "EnteredBy", c => c.String(nullable: false));
        }
    }
}
