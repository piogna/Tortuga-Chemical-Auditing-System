namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntermediateStandardAddPropAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntermediateStandards", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IntermediateStandards", "Amount");
        }
    }
}
