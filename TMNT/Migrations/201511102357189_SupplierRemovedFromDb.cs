namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupplierRemovedFromDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InventoryItems", "Supplier_SupplierId", "dbo.Suppliers");
            DropIndex("dbo.InventoryItems", new[] { "Supplier_SupplierId" });
            DropColumn("dbo.InventoryItems", "Supplier_SupplierId");
            DropTable("dbo.Suppliers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Province = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        FaxNumber = c.String(nullable: false),
                        Website = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ContactPhoneNumber = c.String(),
                        CellNumber = c.String(),
                        Extension = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            AddColumn("dbo.InventoryItems", "Supplier_SupplierId", c => c.Int());
            CreateIndex("dbo.InventoryItems", "Supplier_SupplierId");
            AddForeignKey("dbo.InventoryItems", "Supplier_SupplierId", "dbo.Suppliers", "SupplierId");
        }
    }
}
