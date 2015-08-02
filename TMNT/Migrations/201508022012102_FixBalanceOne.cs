namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixBalanceOne : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeviceVerifications", "VerifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeviceVerifications", "VerifiedOn", c => c.DateTime(nullable: false));
        }
    }
}
