namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubDepartmentField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "SubDepartment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "SubDepartment");
        }
    }
}
