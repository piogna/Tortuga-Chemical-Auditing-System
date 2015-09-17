namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMaxxamStandardLotNumberToMaxxamId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaxxamMadeStandards", "MaxxamId", c => c.String());
            DropColumn("dbo.MaxxamMadeStandards", "LotNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MaxxamMadeStandards", "LotNumber", c => c.String());
            DropColumn("dbo.MaxxamMadeStandards", "MaxxamId");
        }
    }
}
