namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkingStandardChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "WorkingStandard_WorkingStandardId", c => c.Int());
            AddColumn("dbo.PrepListItems", "WorkingStandard_WorkingStandardId", c => c.Int());
            AddColumn("dbo.WorkingStandards", "MaxxamId", c => c.String(nullable: false));
            AddColumn("dbo.WorkingStandards", "Replaces", c => c.String());
            AddColumn("dbo.WorkingStandards", "ReplacedBy", c => c.String());
            AddColumn("dbo.WorkingStandards", "TotalVolume", c => c.String());
            AddColumn("dbo.WorkingStandards", "FinalConcentration", c => c.String());
            AddColumn("dbo.WorkingStandards", "LastModifiedBy", c => c.String());
            AddColumn("dbo.WorkingStandards", "SafetyNotes", c => c.String());
            AddColumn("dbo.WorkingStandards", "FinalVolume", c => c.Int(nullable: false));
            AddColumn("dbo.WorkingStandards", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkingStandards", "DiscardDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.InventoryItems", "WorkingStandard_WorkingStandardId");
            CreateIndex("dbo.PrepListItems", "WorkingStandard_WorkingStandardId");
            AddForeignKey("dbo.InventoryItems", "WorkingStandard_WorkingStandardId", "dbo.WorkingStandards", "WorkingStandardId");
            AddForeignKey("dbo.PrepListItems", "WorkingStandard_WorkingStandardId", "dbo.WorkingStandards", "WorkingStandardId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrepListItems", "WorkingStandard_WorkingStandardId", "dbo.WorkingStandards");
            DropForeignKey("dbo.InventoryItems", "WorkingStandard_WorkingStandardId", "dbo.WorkingStandards");
            DropIndex("dbo.PrepListItems", new[] { "WorkingStandard_WorkingStandardId" });
            DropIndex("dbo.InventoryItems", new[] { "WorkingStandard_WorkingStandardId" });
            DropColumn("dbo.WorkingStandards", "DiscardDate");
            DropColumn("dbo.WorkingStandards", "DateCreated");
            DropColumn("dbo.WorkingStandards", "FinalVolume");
            DropColumn("dbo.WorkingStandards", "SafetyNotes");
            DropColumn("dbo.WorkingStandards", "LastModifiedBy");
            DropColumn("dbo.WorkingStandards", "FinalConcentration");
            DropColumn("dbo.WorkingStandards", "TotalVolume");
            DropColumn("dbo.WorkingStandards", "ReplacedBy");
            DropColumn("dbo.WorkingStandards", "Replaces");
            DropColumn("dbo.WorkingStandards", "MaxxamId");
            DropColumn("dbo.PrepListItems", "WorkingStandard_WorkingStandardId");
            DropColumn("dbo.InventoryItems", "WorkingStandard_WorkingStandardId");
        }
    }
}
