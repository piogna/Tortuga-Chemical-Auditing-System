namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSupplierFieldsToTempReplaceSupplierObject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "SupplierName", c => c.String());
            AddColumn("dbo.StockStandards", "SolventSupplierName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockStandards", "SolventSupplierName");
            DropColumn("dbo.InventoryItems", "SupplierName");
        }
    }
}
