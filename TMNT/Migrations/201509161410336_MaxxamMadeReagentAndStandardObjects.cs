namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxxamMadeReagentAndStandardObjects : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StockStandards", "LotNumber", c => c.String());
            AlterColumn("dbo.StockStandards", "IdCode", c => c.String());
            AlterColumn("dbo.StockStandards", "StockStandardName", c => c.String());
            AlterColumn("dbo.StockStandards", "SolventUsed", c => c.String());
            AlterColumn("dbo.StockStandards", "SolventSupplierName", c => c.String());
            DropColumn("dbo.StockReagents", "LowAmountThreshHold");
            DropColumn("dbo.StockStandards", "LowAmountThreshHold");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockStandards", "LowAmountThreshHold", c => c.Double(nullable: false));
            AddColumn("dbo.StockReagents", "LowAmountThreshHold", c => c.Double(nullable: false));
            AlterColumn("dbo.StockStandards", "SolventSupplierName", c => c.String(nullable: false));
            AlterColumn("dbo.StockStandards", "SolventUsed", c => c.String(nullable: false));
            AlterColumn("dbo.StockStandards", "StockStandardName", c => c.String(nullable: false));
            AlterColumn("dbo.StockStandards", "IdCode", c => c.String(nullable: false));
            AlterColumn("dbo.StockStandards", "LotNumber", c => c.String(nullable: false));
        }
    }
}
