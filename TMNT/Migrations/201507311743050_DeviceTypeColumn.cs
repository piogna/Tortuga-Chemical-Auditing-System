namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceTypeColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "DeviceType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "DeviceType");
        }
    }
}