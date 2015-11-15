namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFinalVolumeWorkingStandard : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WorkingStandards", "FinalVolume");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkingStandards", "FinalVolume", c => c.Int(nullable: false));
        }
    }
}
