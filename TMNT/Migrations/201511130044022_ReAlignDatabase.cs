namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReAlignDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntermediateStandards", "DateOpened", c => c.DateTime());
            AddColumn("dbo.IntermediateStandards", "DaysUntilExpired", c => c.Int());
            AddColumn("dbo.IntermediateStandards", "CreatedBy", c => c.String());
            AddColumn("dbo.IntermediateStandards", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.IntermediateStandards", "ExpiryDate", c => c.DateTime());
            AddColumn("dbo.IntermediateStandards", "DateModified", c => c.DateTime());
            AddColumn("dbo.StockReagents", "Grade", c => c.String());
            AddColumn("dbo.StockReagents", "GradeAdditionalNotes", c => c.String());
            AddColumn("dbo.StockReagents", "DateReceived", c => c.DateTime(nullable: false));
            AddColumn("dbo.StockReagents", "DateOpened", c => c.DateTime());
            AddColumn("dbo.StockReagents", "DaysUntilExpired", c => c.Int());
            AddColumn("dbo.StockReagents", "CreatedBy", c => c.String());
            AddColumn("dbo.StockReagents", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.StockReagents", "ExpiryDate", c => c.DateTime());
            AddColumn("dbo.StockReagents", "DateModified", c => c.DateTime());
            AddColumn("dbo.StockStandards", "DateReceived", c => c.DateTime(nullable: false));
            AddColumn("dbo.StockStandards", "DateOpened", c => c.DateTime());
            AddColumn("dbo.StockStandards", "DaysUntilExpired", c => c.Int());
            AddColumn("dbo.StockStandards", "CreatedBy", c => c.String());
            AddColumn("dbo.StockStandards", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.StockStandards", "ExpiryDate", c => c.DateTime());
            AddColumn("dbo.StockStandards", "DateModified", c => c.DateTime());
            AddColumn("dbo.WorkingStandards", "DateOpened", c => c.DateTime());
            AddColumn("dbo.WorkingStandards", "DaysUntilExpired", c => c.Int());
            AddColumn("dbo.WorkingStandards", "CreatedBy", c => c.String());
            AddColumn("dbo.WorkingStandards", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkingStandards", "ExpiryDate", c => c.DateTime());
            AddColumn("dbo.WorkingStandards", "DateModified", c => c.DateTime());
            AddColumn("dbo.PreparedReagents", "Grade", c => c.String());
            AddColumn("dbo.PreparedReagents", "GradeAdditionalNotes", c => c.String());
            AddColumn("dbo.PreparedReagents", "DateReceived", c => c.DateTime(nullable: false));
            AddColumn("dbo.PreparedReagents", "DateOpened", c => c.DateTime());
            AddColumn("dbo.PreparedReagents", "DaysUntilExpired", c => c.Int());
            AddColumn("dbo.PreparedReagents", "CreatedBy", c => c.String());
            AddColumn("dbo.PreparedReagents", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.PreparedReagents", "ExpiryDate", c => c.DateTime());
            AddColumn("dbo.PreparedReagents", "DateModified", c => c.DateTime());
            AddColumn("dbo.PreparedStandards", "DateReceived", c => c.DateTime(nullable: false));
            AddColumn("dbo.PreparedStandards", "DateOpened", c => c.DateTime());
            AddColumn("dbo.PreparedStandards", "DaysUntilExpired", c => c.Int());
            AddColumn("dbo.PreparedStandards", "CreatedBy", c => c.String());
            AddColumn("dbo.PreparedStandards", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.PreparedStandards", "ExpiryDate", c => c.DateTime());
            AddColumn("dbo.PreparedStandards", "DateModified", c => c.DateTime());
            DropColumn("dbo.InventoryItems", "Grade");
            DropColumn("dbo.InventoryItems", "GradeAdditionalNotes");
            DropColumn("dbo.InventoryItems", "CreatedBy");
            DropColumn("dbo.InventoryItems", "DateOpened");
            DropColumn("dbo.InventoryItems", "DateCreated");
            DropColumn("dbo.InventoryItems", "ExpiryDate");
            DropColumn("dbo.InventoryItems", "DateModified");
            DropColumn("dbo.InventoryItems", "DateReceived");
            DropColumn("dbo.InventoryItems", "DaysUntilExpired");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InventoryItems", "DaysUntilExpired", c => c.Int());
            AddColumn("dbo.InventoryItems", "DateReceived", c => c.DateTime(nullable: false));
            AddColumn("dbo.InventoryItems", "DateModified", c => c.DateTime());
            AddColumn("dbo.InventoryItems", "ExpiryDate", c => c.DateTime());
            AddColumn("dbo.InventoryItems", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.InventoryItems", "DateOpened", c => c.DateTime());
            AddColumn("dbo.InventoryItems", "CreatedBy", c => c.String());
            AddColumn("dbo.InventoryItems", "GradeAdditionalNotes", c => c.String());
            AddColumn("dbo.InventoryItems", "Grade", c => c.String());
            DropColumn("dbo.PreparedStandards", "DateModified");
            DropColumn("dbo.PreparedStandards", "ExpiryDate");
            DropColumn("dbo.PreparedStandards", "DateCreated");
            DropColumn("dbo.PreparedStandards", "CreatedBy");
            DropColumn("dbo.PreparedStandards", "DaysUntilExpired");
            DropColumn("dbo.PreparedStandards", "DateOpened");
            DropColumn("dbo.PreparedStandards", "DateReceived");
            DropColumn("dbo.PreparedReagents", "DateModified");
            DropColumn("dbo.PreparedReagents", "ExpiryDate");
            DropColumn("dbo.PreparedReagents", "DateCreated");
            DropColumn("dbo.PreparedReagents", "CreatedBy");
            DropColumn("dbo.PreparedReagents", "DaysUntilExpired");
            DropColumn("dbo.PreparedReagents", "DateOpened");
            DropColumn("dbo.PreparedReagents", "DateReceived");
            DropColumn("dbo.PreparedReagents", "GradeAdditionalNotes");
            DropColumn("dbo.PreparedReagents", "Grade");
            DropColumn("dbo.WorkingStandards", "DateModified");
            DropColumn("dbo.WorkingStandards", "ExpiryDate");
            DropColumn("dbo.WorkingStandards", "DateCreated");
            DropColumn("dbo.WorkingStandards", "CreatedBy");
            DropColumn("dbo.WorkingStandards", "DaysUntilExpired");
            DropColumn("dbo.WorkingStandards", "DateOpened");
            DropColumn("dbo.StockStandards", "DateModified");
            DropColumn("dbo.StockStandards", "ExpiryDate");
            DropColumn("dbo.StockStandards", "DateCreated");
            DropColumn("dbo.StockStandards", "CreatedBy");
            DropColumn("dbo.StockStandards", "DaysUntilExpired");
            DropColumn("dbo.StockStandards", "DateOpened");
            DropColumn("dbo.StockStandards", "DateReceived");
            DropColumn("dbo.StockReagents", "DateModified");
            DropColumn("dbo.StockReagents", "ExpiryDate");
            DropColumn("dbo.StockReagents", "DateCreated");
            DropColumn("dbo.StockReagents", "CreatedBy");
            DropColumn("dbo.StockReagents", "DaysUntilExpired");
            DropColumn("dbo.StockReagents", "DateOpened");
            DropColumn("dbo.StockReagents", "DateReceived");
            DropColumn("dbo.StockReagents", "GradeAdditionalNotes");
            DropColumn("dbo.StockReagents", "Grade");
            DropColumn("dbo.IntermediateStandards", "DateModified");
            DropColumn("dbo.IntermediateStandards", "ExpiryDate");
            DropColumn("dbo.IntermediateStandards", "DateCreated");
            DropColumn("dbo.IntermediateStandards", "CreatedBy");
            DropColumn("dbo.IntermediateStandards", "DaysUntilExpired");
            DropColumn("dbo.IntermediateStandards", "DateOpened");
        }
    }
}
