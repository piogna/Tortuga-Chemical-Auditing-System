namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSolventSupplier : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StockStandards", "SolventSupplierName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockStandards", "SolventSupplierName", c => c.String());
        }
    }
}
