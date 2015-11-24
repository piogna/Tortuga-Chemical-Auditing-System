namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredOnSomeForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Devices", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DeviceVerifications", "Device_DeviceId", "dbo.Devices");
            DropIndex("dbo.Devices", new[] { "Department_DepartmentId" });
            DropIndex("dbo.DeviceVerifications", new[] { "Device_DeviceId" });
            AlterColumn("dbo.Devices", "Department_DepartmentId", c => c.Int());
            AlterColumn("dbo.DeviceVerifications", "Device_DeviceId", c => c.Int());
            CreateIndex("dbo.Devices", "Department_DepartmentId");
            CreateIndex("dbo.DeviceVerifications", "Device_DeviceId");
            AddForeignKey("dbo.Devices", "Department_DepartmentId", "dbo.Departments", "DepartmentId");
            AddForeignKey("dbo.DeviceVerifications", "Device_DeviceId", "dbo.Devices", "DeviceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceVerifications", "Device_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Devices", "Department_DepartmentId", "dbo.Departments");
            DropIndex("dbo.DeviceVerifications", new[] { "Device_DeviceId" });
            DropIndex("dbo.Devices", new[] { "Department_DepartmentId" });
            AlterColumn("dbo.DeviceVerifications", "Device_DeviceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Devices", "Department_DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.DeviceVerifications", "Device_DeviceId");
            CreateIndex("dbo.Devices", "Department_DepartmentId");
            AddForeignKey("dbo.DeviceVerifications", "Device_DeviceId", "dbo.Devices", "DeviceId", cascadeDelete: true);
            AddForeignKey("dbo.Devices", "Department_DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
        }
    }
}
