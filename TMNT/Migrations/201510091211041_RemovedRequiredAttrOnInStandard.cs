namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredAttrOnInStandard : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IntermediateStandards", "PrepList_PrepListId", "dbo.PrepLists");
            DropIndex("dbo.IntermediateStandards", new[] { "PrepList_PrepListId" });
            AlterColumn("dbo.IntermediateStandards", "PrepList_PrepListId", c => c.Int());
            CreateIndex("dbo.IntermediateStandards", "PrepList_PrepListId");
            AddForeignKey("dbo.IntermediateStandards", "PrepList_PrepListId", "dbo.PrepLists", "PrepListId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IntermediateStandards", "PrepList_PrepListId", "dbo.PrepLists");
            DropIndex("dbo.IntermediateStandards", new[] { "PrepList_PrepListId" });
            AlterColumn("dbo.IntermediateStandards", "PrepList_PrepListId", c => c.Int(nullable: false));
            CreateIndex("dbo.IntermediateStandards", "PrepList_PrepListId");
            AddForeignKey("dbo.IntermediateStandards", "PrepList_PrepListId", "dbo.PrepLists", "PrepListId", cascadeDelete: true);
        }
    }
}
