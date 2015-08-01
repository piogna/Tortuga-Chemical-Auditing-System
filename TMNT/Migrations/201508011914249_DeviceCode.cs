namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "DeviceCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "DeviceCode");
        }
    }
}
