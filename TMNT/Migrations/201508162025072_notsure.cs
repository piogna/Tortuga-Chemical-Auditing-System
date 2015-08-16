namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notsure : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PrepListItems", "StockReagentAmountTaken");
            DropColumn("dbo.PrepListItems", "StockStandardAmountTaken");
            DropColumn("dbo.PrepListItems", "IntermediateStandardAmountTaken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrepListItems", "IntermediateStandardAmountTaken", c => c.Double());
            AddColumn("dbo.PrepListItems", "StockStandardAmountTaken", c => c.Double());
            AddColumn("dbo.PrepListItems", "StockReagentAmountTaken", c => c.Double());
        }
    }
}
