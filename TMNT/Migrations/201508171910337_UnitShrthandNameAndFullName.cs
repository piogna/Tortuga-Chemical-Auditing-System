namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitShrthandNameAndFullName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "UnitShorthandName", c => c.String(nullable: false));
            AddColumn("dbo.Units", "UnitFullName", c => c.String());
            DropColumn("dbo.Units", "UnitName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Units", "UnitName", c => c.String(nullable: false));
            DropColumn("dbo.Units", "UnitFullName");
            DropColumn("dbo.Units", "UnitShorthandName");
        }
    }
}
