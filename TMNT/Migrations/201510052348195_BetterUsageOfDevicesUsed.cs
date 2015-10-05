namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BetterUsageOfDevicesUsed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItems", "FirstDeviceUsed_DeviceId", c => c.Int());
            AddColumn("dbo.InventoryItems", "SecondDeviceUsed_DeviceId", c => c.Int());
            CreateIndex("dbo.InventoryItems", "FirstDeviceUsed_DeviceId");
            CreateIndex("dbo.InventoryItems", "SecondDeviceUsed_DeviceId");
            AddForeignKey("dbo.InventoryItems", "FirstDeviceUsed_DeviceId", "dbo.Devices", "DeviceId");
            AddForeignKey("dbo.InventoryItems", "SecondDeviceUsed_DeviceId", "dbo.Devices", "DeviceId");
            DropColumn("dbo.InventoryItems", "DevicesUsed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InventoryItems", "DevicesUsed", c => c.String());
            DropForeignKey("dbo.InventoryItems", "SecondDeviceUsed_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.InventoryItems", "FirstDeviceUsed_DeviceId", "dbo.Devices");
            DropIndex("dbo.InventoryItems", new[] { "SecondDeviceUsed_DeviceId" });
            DropIndex("dbo.InventoryItems", new[] { "FirstDeviceUsed_DeviceId" });
            DropColumn("dbo.InventoryItems", "SecondDeviceUsed_DeviceId");
            DropColumn("dbo.InventoryItems", "FirstDeviceUsed_DeviceId");
        }
    }
}
