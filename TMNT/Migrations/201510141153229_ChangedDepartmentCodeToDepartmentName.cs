namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDepartmentCodeToDepartmentName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "DepartmentName", c => c.String(nullable: false));
            DropColumn("dbo.Departments", "DepartmentCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "DepartmentCode", c => c.String(nullable: false));
            DropColumn("dbo.Departments", "DepartmentName");
        }
    }
}
