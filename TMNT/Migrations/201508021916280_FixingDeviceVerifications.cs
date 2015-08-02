namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingDeviceVerifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceVerifications", "LastVerified", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeviceVerifications", "WeightOne", c => c.Double());
            AddColumn("dbo.DeviceVerifications", "WeightTwo", c => c.Double());
            AddColumn("dbo.DeviceVerifications", "WeightThree", c => c.Double());
            DropColumn("dbo.Devices", "LastVerified");
            DropColumn("dbo.Devices", "LastVerifiedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "LastVerifiedBy", c => c.String());
            AddColumn("dbo.Devices", "LastVerified", c => c.DateTime(nullable: false));
            DropColumn("dbo.DeviceVerifications", "WeightThree");
            DropColumn("dbo.DeviceVerifications", "WeightTwo");
            DropColumn("dbo.DeviceVerifications", "WeightOne");
            DropColumn("dbo.DeviceVerifications", "LastVerified");
        }
    }
}
