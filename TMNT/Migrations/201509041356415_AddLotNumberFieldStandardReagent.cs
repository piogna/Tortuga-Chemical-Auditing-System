namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLotNumberFieldStandardReagent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockReagents", "LotNumber", c => c.String());
            AddColumn("dbo.StockStandards", "LotNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockStandards", "LotNumber");
            DropColumn("dbo.StockReagents", "LotNumber");
        }
    }
}
