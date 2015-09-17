namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFIeldsToIntermediateStandard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntermediateStandards", "MaxxamId", c => c.String());
            AddColumn("dbo.IntermediateStandards", "FinalVolume", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IntermediateStandards", "FinalVolume");
            DropColumn("dbo.IntermediateStandards", "MaxxamId");
        }
    }
}
