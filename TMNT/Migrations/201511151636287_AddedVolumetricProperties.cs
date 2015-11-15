namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVolumetricProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "VolumetricDeviceType", c => c.String());
            AddColumn("dbo.Devices", "Categorization", c => c.String());
            AddColumn("dbo.Devices", "Frequency", c => c.String());
            AddColumn("dbo.Devices", "Volume", c => c.String());
            AddColumn("dbo.Devices", "AcceptanceCriteria", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "AcceptanceCriteria");
            DropColumn("dbo.Devices", "Volume");
            DropColumn("dbo.Devices", "Frequency");
            DropColumn("dbo.Devices", "Categorization");
            DropColumn("dbo.Devices", "VolumetricDeviceType");
        }
    }
}
