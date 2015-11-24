namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixreagent : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.StockReagents", "Replaces", c => c.Boolean(nullable: false));
            AddColumn("dbo.StockReagents", "ReplacedBy", c => c.String());
            AddColumn("dbo.StockReagents", "IsArchived", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockReagents", "IsArchived");
            DropColumn("dbo.StockReagents", "ReplacedBy");
            DropColumn("dbo.StockReagents", "Replaces");
        }
    }
}
