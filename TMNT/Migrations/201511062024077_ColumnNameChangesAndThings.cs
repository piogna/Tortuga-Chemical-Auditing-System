namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNameChangesAndThings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "AmountLimitOne", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "AmountLimitTwo", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "AmountLimitThree", c => c.Int(nullable: false));
            DropColumn("dbo.Devices", "WeightLimitOne");
            DropColumn("dbo.Devices", "WeightLimitTwo");
            DropColumn("dbo.Devices", "WeightLimitThree");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "WeightLimitThree", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "WeightLimitTwo", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "WeightLimitOne", c => c.Int(nullable: false));
            DropColumn("dbo.Devices", "AmountLimitThree");
            DropColumn("dbo.Devices", "AmountLimitTwo");
            DropColumn("dbo.Devices", "AmountLimitOne");
        }
    }
}
