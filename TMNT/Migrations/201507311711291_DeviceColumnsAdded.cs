namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeviceColumnsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "IsVerified", c => c.Boolean(nullable: false));
            AddColumn("dbo.Devices", "LastVerified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Devices", "LastVerifiedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "LastVerifiedBy");
            DropColumn("dbo.Devices", "LastVerified");
            DropColumn("dbo.Devices", "IsVerified");
        }
    }
}
