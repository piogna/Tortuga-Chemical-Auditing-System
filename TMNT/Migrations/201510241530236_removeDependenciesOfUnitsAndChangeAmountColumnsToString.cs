namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDependenciesOfUnitsAndChangeAmountColumnsToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrepListItems", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.PrepListItems", new[] { "Unit_UnitId" });
            AddColumn("dbo.PrepListItems", "AmountTaken", c => c.String(nullable: false));
            AlterColumn("dbo.IntermediateStandards", "TotalVolume", c => c.String());
            AlterColumn("dbo.IntermediateStandards", "FinalConcentration", c => c.String());
            DropColumn("dbo.PrepListItems", "Amount");
            DropColumn("dbo.PrepListItems", "Unit_UnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrepListItems", "Unit_UnitId", c => c.Int());
            AddColumn("dbo.PrepListItems", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.IntermediateStandards", "FinalConcentration", c => c.Int(nullable: false));
            AlterColumn("dbo.IntermediateStandards", "TotalVolume", c => c.Int(nullable: false));
            DropColumn("dbo.PrepListItems", "AmountTaken");
            CreateIndex("dbo.PrepListItems", "Unit_UnitId");
            AddForeignKey("dbo.PrepListItems", "Unit_UnitId", "dbo.Units", "UnitId");
        }
    }
}
