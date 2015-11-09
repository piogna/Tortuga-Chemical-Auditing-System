namespace TMNT.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class DeviceIsArchived : DbMigration {
        public override void Up() {
            AddColumn("dbo.Devices", "IsArchived", c => c.Boolean(nullable: false));
        }

        public override void Down() {
            DropColumn("dbo.Devices", "IsArchived");
        }
    }
}
