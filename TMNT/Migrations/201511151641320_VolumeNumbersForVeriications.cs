namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VolumeNumbersForVeriications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceVerifications", "VolumeOne", c => c.Double());
            AddColumn("dbo.DeviceVerifications", "VolumeTwo", c => c.Double());
            AddColumn("dbo.DeviceVerifications", "VolumeThree", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceVerifications", "VolumeThree");
            DropColumn("dbo.DeviceVerifications", "VolumeTwo");
            DropColumn("dbo.DeviceVerifications", "VolumeOne");
        }
    }
}
