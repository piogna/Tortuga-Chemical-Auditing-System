namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubDepartmentCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "SubDepartmentCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "SubDepartmentCode");
        }
    }
}
