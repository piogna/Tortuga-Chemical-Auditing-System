namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntermediateStandardSafetyNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntermediateStandards", "SafetyNotes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IntermediateStandards", "SafetyNotes");
        }
    }
}
