namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameObjects : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId", "dbo.MaxxamMadeReagents");
            DropForeignKey("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId", "dbo.MaxxamMadeStandards");
            DropIndex("dbo.InventoryItems", new[] { "MaxxamMadeReagent_MaxxamMadeReagentId" });
            DropIndex("dbo.InventoryItems", new[] { "MaxxamMadeStandard_MaxxamMadeStandardId" });
            CreateTable(
                "dbo.PreparedReagents",
                c => new
                    {
                        PreparedReagentId = c.Int(nullable: false, identity: true),
                        MaxxamId = c.String(),
                        IdCode = c.String(),
                        PreparedReagentName = c.String(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.PreparedReagentId);
            
            CreateTable(
                "dbo.PreparedStandards",
                c => new
                    {
                        PreparedStandardId = c.Int(nullable: false, identity: true),
                        MaxxamId = c.String(),
                        IdCode = c.String(),
                        PreparedStandardName = c.String(),
                        SolventUsed = c.String(),
                        SolventSupplierName = c.String(),
                        Purity = c.Double(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.PreparedStandardId);
            
            AddColumn("dbo.InventoryItems", "PreparedReagent_PreparedReagentId", c => c.Int());
            AddColumn("dbo.InventoryItems", "PreparedStandard_PreparedStandardId", c => c.Int());
            CreateIndex("dbo.InventoryItems", "PreparedReagent_PreparedReagentId");
            CreateIndex("dbo.InventoryItems", "PreparedStandard_PreparedStandardId");
            AddForeignKey("dbo.InventoryItems", "PreparedReagent_PreparedReagentId", "dbo.PreparedReagents", "PreparedReagentId");
            AddForeignKey("dbo.InventoryItems", "PreparedStandard_PreparedStandardId", "dbo.PreparedStandards", "PreparedStandardId");
            DropColumn("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId");
            DropColumn("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId");
            DropTable("dbo.MaxxamMadeReagents");
            DropTable("dbo.MaxxamMadeStandards");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MaxxamMadeStandards",
                c => new
                    {
                        MaxxamMadeStandardId = c.Int(nullable: false, identity: true),
                        MaxxamId = c.String(),
                        IdCode = c.String(),
                        MaxxamMadeStandardName = c.String(),
                        SolventUsed = c.String(),
                        SolventSupplierName = c.String(),
                        Purity = c.Double(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.MaxxamMadeStandardId);
            
            CreateTable(
                "dbo.MaxxamMadeReagents",
                c => new
                    {
                        MaxxamMadeReagentId = c.Int(nullable: false, identity: true),
                        MaxxamId = c.String(),
                        IdCode = c.String(),
                        MaxxamMadeReagentName = c.String(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.MaxxamMadeReagentId);
            
            AddColumn("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId", c => c.Int());
            AddColumn("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId", c => c.Int());
            DropForeignKey("dbo.InventoryItems", "PreparedStandard_PreparedStandardId", "dbo.PreparedStandards");
            DropForeignKey("dbo.InventoryItems", "PreparedReagent_PreparedReagentId", "dbo.PreparedReagents");
            DropIndex("dbo.InventoryItems", new[] { "PreparedStandard_PreparedStandardId" });
            DropIndex("dbo.InventoryItems", new[] { "PreparedReagent_PreparedReagentId" });
            DropColumn("dbo.InventoryItems", "PreparedStandard_PreparedStandardId");
            DropColumn("dbo.InventoryItems", "PreparedReagent_PreparedReagentId");
            DropTable("dbo.PreparedStandards");
            DropTable("dbo.PreparedReagents");
            CreateIndex("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId");
            CreateIndex("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId");
            AddForeignKey("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId", "dbo.MaxxamMadeStandards", "MaxxamMadeStandardId");
            AddForeignKey("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId", "dbo.MaxxamMadeReagents", "MaxxamMadeReagentId");
        }
    }
}
