namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsFirstTimeLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsFirstTimeLogin", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "Biography");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Biography", c => c.String());
            DropColumn("dbo.AspNetUsers", "IsFirstTimeLogin");
        }
    }
}
