namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RebuildDeviceColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "AmountLimitOne", c => c.String(nullable: false));
            AddColumn("dbo.Devices", "AmountLimitTwo", c => c.String(nullable: false));
            AddColumn("dbo.Devices", "AmountLimitThree", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "AmountLimitThree");
            DropColumn("dbo.Devices", "AmountLimitTwo");
            DropColumn("dbo.Devices", "AmountLimitOne");
        }
    }
}
