namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldsToAspNetUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastPasswordChange", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "NextRequiredPasswordChange", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Biography", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Biography");
            DropColumn("dbo.AspNetUsers", "NextRequiredPasswordChange");
            DropColumn("dbo.AspNetUsers", "LastPasswordChange");
        }
    }
}
