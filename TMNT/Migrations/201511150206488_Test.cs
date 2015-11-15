namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockReagents", "Department_DepartmentId", "dbo.Departments");
            DropIndex("dbo.StockReagents", new[] { "Department_DepartmentId" });
            DropColumn("dbo.StockReagents", "Department_DepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StockReagents", "Department_DepartmentId", c => c.Int());
            CreateIndex("dbo.StockReagents", "Department_DepartmentId");
            AddForeignKey("dbo.StockReagents", "Department_DepartmentId", "dbo.Departments", "DepartmentId");
        }
    }
}
