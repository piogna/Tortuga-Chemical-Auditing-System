namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveReplacesAndReplacedByWorkingStandard : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WorkingStandards", "Replaces");
            DropColumn("dbo.WorkingStandards", "ReplacedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkingStandards", "ReplacedBy", c => c.String());
            AddColumn("dbo.WorkingStandards", "Replaces", c => c.String());
        }
    }
}
