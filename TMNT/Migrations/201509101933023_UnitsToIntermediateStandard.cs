namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitsToIntermediateStandard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntermediateStandards", "Unit_UnitId", c => c.Int());
            CreateIndex("dbo.IntermediateStandards", "Unit_UnitId");
            AddForeignKey("dbo.IntermediateStandards", "Unit_UnitId", "dbo.Units", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IntermediateStandards", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.IntermediateStandards", new[] { "Unit_UnitId" });
            DropColumn("dbo.IntermediateStandards", "Unit_UnitId");
        }
    }
}
