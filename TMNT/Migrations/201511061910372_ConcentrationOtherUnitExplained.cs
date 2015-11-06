namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConcentrationOtherUnitExplained : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "ConcentrationOtherUnitExplained", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "ConcentrationOtherUnitExplained");
        }
    }
}
