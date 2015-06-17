namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CertificateOfAnalysis",
                c => new
                    {
                        CertificateOfAnalysisId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        DateAdded = c.DateTime(nullable: false),
                        InventoryItem_InventoryItemId = c.Int(),
                    })
                .PrimaryKey(t => t.CertificateOfAnalysisId)
                .ForeignKey("dbo.InventoryItems", t => t.InventoryItem_InventoryItemId)
                .Index(t => t.InventoryItem_InventoryItemId);
            
            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        InventoryItemId = c.Int(nullable: false, identity: true),
                        CatalogueCode = c.String(nullable: false),
                        Amount = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        CaseNumber = c.Int(nullable: false),
                        UsedFor = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        Department_DepartmentId = c.Int(),
                        InventoryLocation_InventoryLocationId = c.Int(),
                        StockReagent_ReagentId = c.Int(),
                        StockStandard_StockStandardId = c.Int(),
                        Supplier_SupplierId = c.Int(),
                        Unit_UnitId = c.Int(),
                    })
                .PrimaryKey(t => t.InventoryItemId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .ForeignKey("dbo.InventoryLocations", t => t.InventoryLocation_InventoryLocationId)
                .ForeignKey("dbo.StockReagents", t => t.StockReagent_ReagentId)
                .ForeignKey("dbo.StockStandards", t => t.StockStandard_StockStandardId)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_SupplierId)
                .ForeignKey("dbo.Units", t => t.Unit_UnitId)
                .Index(t => t.Department_DepartmentId)
                .Index(t => t.InventoryLocation_InventoryLocationId)
                .Index(t => t.StockReagent_ReagentId)
                .Index(t => t.StockStandard_StockStandardId)
                .Index(t => t.Supplier_SupplierId)
                .Index(t => t.Unit_UnitId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentCode = c.String(nullable: false),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.Location_LocationId);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        DeviceId = c.Int(nullable: false, identity: true),
                        Department_DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeviceId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId, cascadeDelete: true)
                .Index(t => t.Department_DepartmentId);
            
            CreateTable(
                "dbo.DeviceVerifications",
                c => new
                    {
                        DeviceVerificationId = c.Int(nullable: false, identity: true),
                        Device_DeviceId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.DeviceVerificationId)
                .ForeignKey("dbo.Devices", t => t.Device_DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Device_DeviceId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.DeviceTests",
                c => new
                    {
                        DeviceTestId = c.Int(nullable: false, identity: true),
                        DeviceVerification_DeviceVerificationId = c.Int(nullable: false),
                        DeviceVerification_DeviceVerificationId1 = c.Int(),
                    })
                .PrimaryKey(t => t.DeviceTestId)
                .ForeignKey("dbo.DeviceVerifications", t => t.DeviceVerification_DeviceVerificationId, cascadeDelete: true)
                .ForeignKey("dbo.DeviceVerifications", t => t.DeviceVerification_DeviceVerificationId1)
                .Index(t => t.DeviceVerification_DeviceVerificationId)
                .Index(t => t.DeviceVerification_DeviceVerificationId1);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Department_DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Department_DepartmentId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        UserSettingsId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Language = c.String(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserSettingsId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        LocationCode = c.String(nullable: false),
                        LocationName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Province = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        FaxNumber = c.String(nullable: false),
                        Website = c.String(nullable: false),
                        InventoryItem_InventoryItemId = c.Int(),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.InventoryItems", t => t.InventoryItem_InventoryItemId)
                .Index(t => t.InventoryItem_InventoryItemId);
            
            CreateTable(
                "dbo.InventoryLocations",
                c => new
                    {
                        InventoryLocationId = c.Int(nullable: false, identity: true),
                        InventoryLocationName = c.String(nullable: false),
                        Location_LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryLocationId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId, cascadeDelete: true)
                .Index(t => t.Location_LocationId);
            
            CreateTable(
                "dbo.MSDS",
                c => new
                    {
                        MSDSId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        DateAdded = c.DateTime(nullable: false),
                        InventoryItem_InventoryItemId = c.Int(),
                    })
                .PrimaryKey(t => t.MSDSId)
                .ForeignKey("dbo.InventoryItems", t => t.InventoryItem_InventoryItemId)
                .Index(t => t.InventoryItem_InventoryItemId);
            
            CreateTable(
                "dbo.SpikingStandards",
                c => new
                    {
                        SurrogateSpikingId = c.Int(nullable: false, identity: true),
                        Replaces = c.String(),
                        ReplacedBy = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DiscardDate = c.DateTime(nullable: false),
                        SpikeVolume = c.Double(nullable: false),
                        SyringeId_DeviceId = c.Int(nullable: false),
                        InventoryItem_InventoryItemId = c.Int(),
                    })
                .PrimaryKey(t => t.SurrogateSpikingId)
                .ForeignKey("dbo.Devices", t => t.SyringeId_DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.InventoryItems", t => t.InventoryItem_InventoryItemId)
                .Index(t => t.SyringeId_DeviceId)
                .Index(t => t.InventoryItem_InventoryItemId);
            
            CreateTable(
                "dbo.StockReagents",
                c => new
                    {
                        ReagentId = c.Int(nullable: false, identity: true),
                        IdCode = c.String(nullable: false),
                        DateEntered = c.DateTime(nullable: false),
                        ReagentName = c.String(nullable: false),
                        EnteredBy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReagentId);
            
            CreateTable(
                "dbo.StockStandards",
                c => new
                    {
                        StockStandardId = c.Int(nullable: false, identity: true),
                        IdCode = c.String(nullable: false),
                        StockStandardName = c.String(nullable: false),
                        DateEntered = c.DateTime(nullable: false),
                        SolventUsed = c.String(nullable: false),
                        Purity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StockStandardId);
            
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
            
            CreateTable(
                "dbo.SurrogateSpikingStandards",
                c => new
                    {
                        SurrogateSpikingId = c.Int(nullable: false, identity: true),
                        Replaces = c.String(),
                        ReplacedBy = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DiscardDate = c.DateTime(nullable: false),
                        SpikeVolume = c.Double(nullable: false),
                        SyringeId_DeviceId = c.Int(nullable: false),
                        InventoryItem_InventoryItemId = c.Int(),
                    })
                .PrimaryKey(t => t.SurrogateSpikingId)
                .ForeignKey("dbo.Devices", t => t.SyringeId_DeviceId, cascadeDelete: true)
                .ForeignKey("dbo.InventoryItems", t => t.InventoryItem_InventoryItemId)
                .Index(t => t.SyringeId_DeviceId)
                .Index(t => t.InventoryItem_InventoryItemId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        UnitName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UnitId);
            
            CreateTable(
                "dbo.IntermediateStandards",
                c => new
                    {
                        IntermediateStandardId = c.Int(nullable: false, identity: true),
                        IdCode = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DiscardDate = c.DateTime(nullable: false),
                        Replaces = c.Int(),
                        ReplacedBy = c.Int(),
                        PrepList_PrepListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IntermediateStandardId)
                .ForeignKey("dbo.PrepLists", t => t.PrepList_PrepListId, cascadeDelete: true)
                .Index(t => t.PrepList_PrepListId);
            
            CreateTable(
                "dbo.PrepLists",
                c => new
                    {
                        PrepListId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PrepListId);
            
            CreateTable(
                "dbo.PrepListItems",
                c => new
                    {
                        PrepListItemId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        IntermediateStandard_IntermediateStandardId = c.Int(),
                        PrepList_PrepListId = c.Int(nullable: false),
                        StockReagent_ReagentId = c.Int(),
                        StockStandard_StockStandardId = c.Int(),
                    })
                .PrimaryKey(t => t.PrepListItemId)
                .ForeignKey("dbo.IntermediateStandards", t => t.IntermediateStandard_IntermediateStandardId)
                .ForeignKey("dbo.PrepLists", t => t.PrepList_PrepListId, cascadeDelete: true)
                .ForeignKey("dbo.StockReagents", t => t.StockReagent_ReagentId)
                .ForeignKey("dbo.StockStandards", t => t.StockStandard_StockStandardId)
                .Index(t => t.IntermediateStandard_IntermediateStandardId)
                .Index(t => t.PrepList_PrepListId)
                .Index(t => t.StockReagent_ReagentId)
                .Index(t => t.StockStandard_StockStandardId);
            
            CreateTable(
                "dbo.WorkingStandards",
                c => new
                    {
                        WorkingStandardId = c.Int(nullable: false, identity: true),
                        IdCode = c.String(nullable: false),
                        PreparationDate = c.DateTime(nullable: false),
                        Source = c.Int(nullable: false),
                        Grade = c.Double(nullable: false),
                        PrepList_PrepListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkingStandardId)
                .ForeignKey("dbo.PrepLists", t => t.PrepList_PrepListId, cascadeDelete: true)
                .Index(t => t.PrepList_PrepListId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.IntermediateStandards", "PrepList_PrepListId", "dbo.PrepLists");
            DropForeignKey("dbo.WorkingStandards", "PrepList_PrepListId", "dbo.PrepLists");
            DropForeignKey("dbo.PrepListItems", "StockStandard_StockStandardId", "dbo.StockStandards");
            DropForeignKey("dbo.PrepListItems", "StockReagent_ReagentId", "dbo.StockReagents");
            DropForeignKey("dbo.PrepListItems", "PrepList_PrepListId", "dbo.PrepLists");
            DropForeignKey("dbo.PrepListItems", "IntermediateStandard_IntermediateStandardId", "dbo.IntermediateStandards");
            DropForeignKey("dbo.InventoryItems", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.SurrogateSpikingStandards", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropForeignKey("dbo.SurrogateSpikingStandards", "SyringeId_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.InventoryItems", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.InventoryItems", "StockStandard_StockStandardId", "dbo.StockStandards");
            DropForeignKey("dbo.InventoryItems", "StockReagent_ReagentId", "dbo.StockReagents");
            DropForeignKey("dbo.SpikingStandards", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropForeignKey("dbo.SpikingStandards", "SyringeId_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.MSDS", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropForeignKey("dbo.Locations", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropForeignKey("dbo.InventoryLocations", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.InventoryItems", "InventoryLocation_InventoryLocationId", "dbo.InventoryLocations");
            DropForeignKey("dbo.Departments", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.InventoryItems", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DeviceVerifications", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSettings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DeviceVerifications", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DeviceTests", "DeviceVerification_DeviceVerificationId1", "dbo.DeviceVerifications");
            DropForeignKey("dbo.DeviceTests", "DeviceVerification_DeviceVerificationId", "dbo.DeviceVerifications");
            DropForeignKey("dbo.DeviceVerifications", "Device_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Devices", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CertificateOfAnalysis", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.WorkingStandards", new[] { "PrepList_PrepListId" });
            DropIndex("dbo.PrepListItems", new[] { "StockStandard_StockStandardId" });
            DropIndex("dbo.PrepListItems", new[] { "StockReagent_ReagentId" });
            DropIndex("dbo.PrepListItems", new[] { "PrepList_PrepListId" });
            DropIndex("dbo.PrepListItems", new[] { "IntermediateStandard_IntermediateStandardId" });
            DropIndex("dbo.IntermediateStandards", new[] { "PrepList_PrepListId" });
            DropIndex("dbo.SurrogateSpikingStandards", new[] { "InventoryItem_InventoryItemId" });
            DropIndex("dbo.SurrogateSpikingStandards", new[] { "SyringeId_DeviceId" });
            DropIndex("dbo.SpikingStandards", new[] { "InventoryItem_InventoryItemId" });
            DropIndex("dbo.SpikingStandards", new[] { "SyringeId_DeviceId" });
            DropIndex("dbo.MSDS", new[] { "InventoryItem_InventoryItemId" });
            DropIndex("dbo.InventoryLocations", new[] { "Location_LocationId" });
            DropIndex("dbo.Locations", new[] { "InventoryItem_InventoryItemId" });
            DropIndex("dbo.UserSettings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Department_DepartmentId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.DeviceTests", new[] { "DeviceVerification_DeviceVerificationId1" });
            DropIndex("dbo.DeviceTests", new[] { "DeviceVerification_DeviceVerificationId" });
            DropIndex("dbo.DeviceVerifications", new[] { "User_Id" });
            DropIndex("dbo.DeviceVerifications", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.DeviceVerifications", new[] { "Device_DeviceId" });
            DropIndex("dbo.Devices", new[] { "Department_DepartmentId" });
            DropIndex("dbo.Departments", new[] { "Location_LocationId" });
            DropIndex("dbo.InventoryItems", new[] { "Unit_UnitId" });
            DropIndex("dbo.InventoryItems", new[] { "Supplier_SupplierId" });
            DropIndex("dbo.InventoryItems", new[] { "StockStandard_StockStandardId" });
            DropIndex("dbo.InventoryItems", new[] { "StockReagent_ReagentId" });
            DropIndex("dbo.InventoryItems", new[] { "InventoryLocation_InventoryLocationId" });
            DropIndex("dbo.InventoryItems", new[] { "Department_DepartmentId" });
            DropIndex("dbo.CertificateOfAnalysis", new[] { "InventoryItem_InventoryItemId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.WorkingStandards");
            DropTable("dbo.PrepListItems");
            DropTable("dbo.PrepLists");
            DropTable("dbo.IntermediateStandards");
            DropTable("dbo.Units");
            DropTable("dbo.SurrogateSpikingStandards");
            DropTable("dbo.Suppliers");
            DropTable("dbo.StockStandards");
            DropTable("dbo.StockReagents");
            DropTable("dbo.SpikingStandards");
            DropTable("dbo.MSDS");
            DropTable("dbo.InventoryLocations");
            DropTable("dbo.Locations");
            DropTable("dbo.UserSettings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.DeviceTests");
            DropTable("dbo.DeviceVerifications");
            DropTable("dbo.Devices");
            DropTable("dbo.Departments");
            DropTable("dbo.InventoryItems");
            DropTable("dbo.CertificateOfAnalysis");
        }
    }
}
