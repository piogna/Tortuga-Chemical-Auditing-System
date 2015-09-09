namespace TMNT.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class DeviceTestPassOrFailField : DbMigration {
        public override void Up() {
            AddColumn("dbo.DeviceVerifications", "DidTestPass", c => c.Boolean(nullable: false));
        }

        public override void Down() {
            DropColumn("dbo.DeviceVerifications", "DidTestPass");
        }
    }
}
