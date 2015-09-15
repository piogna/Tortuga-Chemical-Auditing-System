namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrepListItemUnitForeignKeyFixPartOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrepListItems", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.PrepListItems", new[] { "Unit_UnitId" });
            DropColumn("dbo.PrepListItems", "Unit_UnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrepListItems", "Unit_UnitId", c => c.Int());
            CreateIndex("dbo.PrepListItems", "Unit_UnitId");
            AddForeignKey("dbo.PrepListItems", "Unit_UnitId", "dbo.Units", "UnitId");
        }
    }
}
