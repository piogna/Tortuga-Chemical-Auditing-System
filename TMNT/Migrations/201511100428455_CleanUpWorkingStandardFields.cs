namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanUpWorkingStandardFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WorkingStandards", "PreparationDate");
            DropColumn("dbo.WorkingStandards", "Source");
            DropColumn("dbo.WorkingStandards", "Grade");
            DropColumn("dbo.WorkingStandards", "DateCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkingStandards", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkingStandards", "Grade", c => c.Double(nullable: false));
            AddColumn("dbo.WorkingStandards", "Source", c => c.Int(nullable: false));
            AddColumn("dbo.WorkingStandards", "PreparationDate", c => c.DateTime(nullable: false));
        }
    }
}
