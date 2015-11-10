namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeightIdField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "WeightId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "WeightId");
        }
    }
}
