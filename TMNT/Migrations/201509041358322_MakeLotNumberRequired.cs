namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeLotNumberRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StockReagents", "LotNumber", c => c.String(nullable: false));
            AlterColumn("dbo.StockStandards", "LotNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StockStandards", "LotNumber", c => c.String());
            AlterColumn("dbo.StockReagents", "LotNumber", c => c.String());
        }
    }
}