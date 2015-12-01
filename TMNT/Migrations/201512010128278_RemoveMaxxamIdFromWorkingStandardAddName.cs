namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMaxxamIdFromWorkingStandardAddName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkingStandards", "WorkingStandardName", c => c.String());
            AlterColumn("dbo.WorkingStandards", "IdCode", c => c.String());
            DropColumn("dbo.WorkingStandards", "MaxxamId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkingStandards", "MaxxamId", c => c.String(nullable: false));
            AlterColumn("dbo.WorkingStandards", "IdCode", c => c.String(nullable: false));
            DropColumn("dbo.WorkingStandards", "WorkingStandardName");
        }
    }
}
