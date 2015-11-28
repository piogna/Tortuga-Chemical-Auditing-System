namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BalanceType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "BalanceType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "BalanceType");
        }
    }
}
