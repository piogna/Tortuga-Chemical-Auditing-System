namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGradeFromIntToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryItems", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryItems", "Grade", c => c.Int());
        }
    }
}