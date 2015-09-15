namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSDSExpiryDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MSDS", "MSDSExpiryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MSDS", "MSDSExpiryDate", c => c.DateTime(nullable: false));
        }
    }
}
