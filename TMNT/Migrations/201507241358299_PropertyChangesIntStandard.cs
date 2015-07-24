namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyChangesIntStandard : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IntermediateStandards", "Replaces", c => c.String());
            AlterColumn("dbo.IntermediateStandards", "ReplacedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntermediateStandards", "ReplacedBy", c => c.Int());
            AlterColumn("dbo.IntermediateStandards", "Replaces", c => c.Int());
        }
    }
}
