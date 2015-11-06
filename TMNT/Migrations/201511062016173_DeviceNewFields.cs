namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "NumberOfDecimals", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "WeightLimitOne", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "WeightLimitTwo", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "WeightLimitThree", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "WeightLimitThree");
            DropColumn("dbo.Devices", "WeightLimitTwo");
            DropColumn("dbo.Devices", "WeightLimitOne");
            DropColumn("dbo.Devices", "NumberOfDecimals");
        }
    }
}
