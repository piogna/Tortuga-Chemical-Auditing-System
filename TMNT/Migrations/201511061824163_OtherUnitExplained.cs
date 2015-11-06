namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OtherUnitExplained : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "OtherUnitExplained", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "OtherUnitExplained");
        }
    }
}
