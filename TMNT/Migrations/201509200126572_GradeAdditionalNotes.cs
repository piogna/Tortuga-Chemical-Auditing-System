namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradeAdditionalNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "GradeAdditionalNotes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryItems", "GradeAdditionalNotes");
        }
    }
}
