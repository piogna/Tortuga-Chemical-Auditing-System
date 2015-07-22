namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixIntermediateStandards : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IntermediateStandards", "DiscardDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IntermediateStandards", "DiscardDate", c => c.DateTime(nullable: false));
        }
    }
}
