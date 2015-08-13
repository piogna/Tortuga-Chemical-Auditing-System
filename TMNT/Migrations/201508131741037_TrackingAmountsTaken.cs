namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrackingAmountsTaken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrepListItems", "StockReagentAmountTaken", c => c.Double());
            AddColumn("dbo.PrepListItems", "StockStandardAmountTaken", c => c.Double());
            AddColumn("dbo.PrepListItems", "IntermediateStandardAmountTaken", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrepListItems", "IntermediateStandardAmountTaken");
            DropColumn("dbo.PrepListItems", "StockStandardAmountTaken");
            DropColumn("dbo.PrepListItems", "StockReagentAmountTaken");
        }
    }
}
