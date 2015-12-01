namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIntStandardMaxxamIdAddName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntermediateStandards", "IntermediateStandardName", c => c.String());
            DropColumn("dbo.IntermediateStandards", "MaxxamId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IntermediateStandards", "MaxxamId", c => c.String());
            DropColumn("dbo.IntermediateStandards", "IntermediateStandardName");
        }
    }
}
