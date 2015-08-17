namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "UnitType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Units", "UnitType");
        }
    }
}
