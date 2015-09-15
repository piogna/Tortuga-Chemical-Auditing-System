namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCaseNumber : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InventoryItems", "CaseNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InventoryItems", "CaseNumber", c => c.Int(nullable: false));
        }
    }
}
