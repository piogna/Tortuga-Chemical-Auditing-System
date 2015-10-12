namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredOnLocationProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Locations", "LocationCode", c => c.String());
            AlterColumn("dbo.Locations", "LocationName", c => c.String());
            AlterColumn("dbo.Locations", "Address", c => c.String());
            AlterColumn("dbo.Locations", "City", c => c.String());
            AlterColumn("dbo.Locations", "Province", c => c.String());
            AlterColumn("dbo.Locations", "PostalCode", c => c.String());
            AlterColumn("dbo.Locations", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Locations", "FaxNumber", c => c.String());
            AlterColumn("dbo.Locations", "Website", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Locations", "Website", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "FaxNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "Province", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "LocationName", c => c.String(nullable: false));
            AlterColumn("dbo.Locations", "LocationCode", c => c.String(nullable: false));
        }
    }
}
