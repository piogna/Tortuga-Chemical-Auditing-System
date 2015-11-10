namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WorkingStandardRemovedDiscardDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WorkingStandards", "DiscardDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkingStandards", "DiscardDate", c => c.DateTime(nullable: false));
        }
    }
}
