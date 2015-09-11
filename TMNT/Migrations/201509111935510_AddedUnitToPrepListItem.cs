namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUnitToPrepListItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrepListItems", "Unit_UnitId", c => c.Int());
            CreateIndex("dbo.PrepListItems", "Unit_UnitId");
            AddForeignKey("dbo.PrepListItems", "Unit_UnitId", "dbo.Units", "UnitId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrepListItems", "Unit_UnitId", "dbo.Units");
            DropIndex("dbo.PrepListItems", new[] { "Unit_UnitId" });
            DropColumn("dbo.PrepListItems", "Unit_UnitId");
        }
    }
}
