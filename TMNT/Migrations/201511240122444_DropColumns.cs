namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropColumns : DbMigration
    {
        public override void Up() {

            
            DropColumn("dbo.StockReagents", "IsArchived");
            DropColumn("dbo.StockReagents", "ReplacedBy");
            DropColumn("dbo.StockReagents", "Replaces");
        }

        public override void Down() {
            AddColumn("dbo.StockReagents", "Replaces", c => c.Boolean(nullable: false));
            AddColumn("dbo.StockReagents", "ReplacedBy", c => c.String());
            AddColumn("dbo.StockReagents", "IsArchived", c => c.String());
        }
    }
}
