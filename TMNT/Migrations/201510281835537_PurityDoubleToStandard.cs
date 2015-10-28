namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurityDoubleToStandard : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StockStandards", "Purity", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StockStandards", "Purity", c => c.Double(nullable: false));
        }
    }
}
