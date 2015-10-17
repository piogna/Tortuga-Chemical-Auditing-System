namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberOfBottles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "NumberOfBottles", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "NumberOfBottles");
        }
    }
}
