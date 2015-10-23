namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StandardConcentrationField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockStandards", "Concentration", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockStandards", "Concentration");
        }
    }
}
