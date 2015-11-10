namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberOfTestsToVerifyBalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "NumberOfTestsToVerify", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "NumberOfTestsToVerify");
        }
    }
}
