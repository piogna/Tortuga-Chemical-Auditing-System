namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropDeviceColumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Devices", "AmountLimitOne");
            DropColumn("dbo.Devices", "AmountLimitTwo");
            DropColumn("dbo.Devices", "AmountLimitThree");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "AmountLimitThree", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "AmountLimitTwo", c => c.Int(nullable: false));
            AddColumn("dbo.Devices", "AmountLimitOne", c => c.Int(nullable: false));
        }
    }
}
