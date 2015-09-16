namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaxxamMadeReagents",
                c => new
                    {
                        MaxxamMadeReagentId = c.Int(nullable: false, identity: true),
                        LotNumber = c.String(),
                        IdCode = c.String(),
                        MaxxamMadeReagentName = c.String(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.MaxxamMadeReagentId);
            
            CreateTable(
                "dbo.MaxxamMadeStandards",
                c => new
                    {
                        MaxxamMadeStandardId = c.Int(nullable: false, identity: true),
                        LotNumber = c.String(),
                        IdCode = c.String(),
                        MaxxamMadeStandardName = c.String(),
                        SolventUsed = c.String(),
                        SolventSupplierName = c.String(),
                        Purity = c.Double(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.MaxxamMadeStandardId);
            
            AddColumn("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId", c => c.Int());
            AddColumn("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId", c => c.Int());
            CreateIndex("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId");
            CreateIndex("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId");
            AddForeignKey("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId", "dbo.MaxxamMadeReagents", "MaxxamMadeReagentId");
            AddForeignKey("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId", "dbo.MaxxamMadeStandards", "MaxxamMadeStandardId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId", "dbo.MaxxamMadeStandards");
            DropForeignKey("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId", "dbo.MaxxamMadeReagents");
            DropIndex("dbo.InventoryItems", new[] { "MaxxamMadeStandard_MaxxamMadeStandardId" });
            DropIndex("dbo.InventoryItems", new[] { "MaxxamMadeReagent_MaxxamMadeReagentId" });
            DropColumn("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId");
            DropColumn("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId");
            DropTable("dbo.MaxxamMadeStandards");
            DropTable("dbo.MaxxamMadeReagents");
        }
    }
}
