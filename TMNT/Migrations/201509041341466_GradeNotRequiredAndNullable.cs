namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradeNotRequiredAndNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InventoryItems", "Grade", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InventoryItems", "Grade", c => c.Int(nullable: false));
        }
    }
}
