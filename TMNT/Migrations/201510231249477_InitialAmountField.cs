namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialAmountField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "InitialAmount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "InitialAmount");
        }
    }
}
