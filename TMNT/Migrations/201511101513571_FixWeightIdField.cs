namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixWeightIdField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceVerifications", "WeightId", c => c.String());
            DropColumn("dbo.Devices", "WeightId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "WeightId", c => c.String());
            DropColumn("dbo.DeviceVerifications", "WeightId");
        }
    }
}
