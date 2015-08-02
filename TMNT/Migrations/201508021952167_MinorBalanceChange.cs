namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinorBalanceChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceVerifications", "VerifiedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.DeviceVerifications", "LastVerified");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceVerifications", "LastVerified", c => c.DateTime(nullable: false));
            DropColumn("dbo.DeviceVerifications", "VerifiedOn");
        }
    }
}
