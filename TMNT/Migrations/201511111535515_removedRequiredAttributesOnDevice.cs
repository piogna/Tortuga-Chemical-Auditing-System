namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedRequiredAttributesOnDevice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Devices", "AmountLimitOne", c => c.String());
            AlterColumn("dbo.Devices", "AmountLimitTwo", c => c.String());
            AlterColumn("dbo.Devices", "AmountLimitThree", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devices", "AmountLimitThree", c => c.String(nullable: false));
            AlterColumn("dbo.Devices", "AmountLimitTwo", c => c.String(nullable: false));
            AlterColumn("dbo.Devices", "AmountLimitOne", c => c.String(nullable: false));
        }
    }
}
