namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvevntoryItemUpdateAndReagentFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaxxamMadeReagents", "MaxxamId", c => c.String());
            DropColumn("dbo.MaxxamMadeReagents", "LotNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MaxxamMadeReagents", "LotNumber", c => c.String());
            DropColumn("dbo.MaxxamMadeReagents", "MaxxamId");
        }
    }
}
