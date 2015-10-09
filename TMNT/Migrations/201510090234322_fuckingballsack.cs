namespace TMNT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fuckingballsack : DbMigration
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
                        CatalogueCode = c.String(),
                        LowAmountThreshHold = c.Double(nullable: false),
                        Grade = c.String(),
                        GradeAdditionalNotes = c.String(),
                        UsedFor = c.String(),
                        Type = c.String(),
                        SupplierName = c.String(),
                        CreatedBy = c.String(),
                        DateOpened = c.DateTime(),
                        DateCreated = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        StorageRequirements = c.String(),
                        Department_DepartmentId = c.Int(),
                        InventoryLocation_InventoryLocationId = c.Int(),
                        FirstDeviceUsed_DeviceId = c.Int(),
                        IntermediateStandard_IntermediateStandardId = c.Int(),
                        StockReagent_ReagentId = c.Int(),
                        StockStandard_StockStandardId = c.Int(),
                        Unit_UnitId = c.Int(),
                        MaxxamMadeReagent_MaxxamMadeReagentId = c.Int(),
                        MaxxamMadeStandard_MaxxamMadeStandardId = c.Int(),
                        SecondDeviceUsed_DeviceId = c.Int(),
                        Supplier_SupplierId = c.Int(),
                    })
                .PrimaryKey(t => t.InventoryItemId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .ForeignKey("dbo.InventoryLocations", t => t.InventoryLocation_InventoryLocationId)
                .ForeignKey("dbo.Devices", t => t.FirstDeviceUsed_DeviceId)
                .ForeignKey("dbo.IntermediateStandards", t => t.IntermediateStandard_IntermediateStandardId)
                .ForeignKey("dbo.StockReagents", t => t.StockReagent_ReagentId)
                .ForeignKey("dbo.StockStandards", t => t.StockStandard_StockStandardId)
                .ForeignKey("dbo.Units", t => t.Unit_UnitId)
                .ForeignKey("dbo.MaxxamMadeReagents", t => t.MaxxamMadeReagent_MaxxamMadeReagentId)
                .ForeignKey("dbo.MaxxamMadeStandards", t => t.MaxxamMadeStandard_MaxxamMadeStandardId)
                .ForeignKey("dbo.Devices", t => t.SecondDeviceUsed_DeviceId)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_SupplierId)
                .Index(t => t.Department_DepartmentId)
                .Index(t => t.InventoryLocation_InventoryLocationId)
                .Index(t => t.FirstDeviceUsed_DeviceId)
                .Index(t => t.IntermediateStandard_IntermediateStandardId)
                .Index(t => t.StockReagent_ReagentId)
                .Index(t => t.StockStandard_StockStandardId)
                .Index(t => t.Unit_UnitId)
                .Index(t => t.MaxxamMadeReagent_MaxxamMadeReagentId)
                .Index(t => t.MaxxamMadeStandard_MaxxamMadeStandardId)
                .Index(t => t.SecondDeviceUsed_DeviceId)
                .Index(t => t.Supplier_SupplierId);
            
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
                        DeviceCode = c.String(),
                        IsVerified = c.Boolean(nullable: false),
                        DeviceType = c.String(),
                        Status = c.String(),
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
                        VerifiedOn = c.DateTime(),
                        WeightOne = c.Double(),
                        WeightTwo = c.Double(),
                        WeightThree = c.Double(),
                        DidTestPass = c.Boolean(nullable: false),
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
                        LastPasswordChange = c.DateTime(),
                        NextRequiredPasswordChange = c.DateTime(),
                        Biography = c.String(),
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
                "dbo.IntermediateStandards",
                c => new
                    {
                        IntermediateStandardId = c.Int(nullable: false, identity: true),
                        IdCode = c.String(),
                        Replaces = c.String(),
                        MaxxamId = c.String(),
                        FinalVolume = c.Int(nullable: false),
                        ReplacedBy = c.String(),
                        TotalVolume = c.Int(nullable: false),
                        FinalConcentration = c.Int(nullable: false),
                        LastModifiedBy = c.String(),
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
                        Unit_UnitId = c.Int(),
                    })
                .PrimaryKey(t => t.PrepListItemId)
                .ForeignKey("dbo.IntermediateStandards", t => t.IntermediateStandard_IntermediateStandardId)
                .ForeignKey("dbo.PrepLists", t => t.PrepList_PrepListId, cascadeDelete: true)
                .ForeignKey("dbo.StockReagents", t => t.StockReagent_ReagentId)
                .ForeignKey("dbo.StockStandards", t => t.StockStandard_StockStandardId)
                .ForeignKey("dbo.Units", t => t.Unit_UnitId)
                .Index(t => t.IntermediateStandard_IntermediateStandardId)
                .Index(t => t.PrepList_PrepListId)
                .Index(t => t.StockReagent_ReagentId)
                .Index(t => t.StockStandard_StockStandardId)
                .Index(t => t.Unit_UnitId);
            
            CreateTable(
                "dbo.StockReagents",
                c => new
                    {
                        ReagentId = c.Int(nullable: false, identity: true),
                        LotNumber = c.String(),
                        IdCode = c.String(),
                        ReagentName = c.String(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ReagentId);
            
            CreateTable(
                "dbo.StockStandards",
                c => new
                    {
                        StockStandardId = c.Int(nullable: false, identity: true),
                        LotNumber = c.String(),
                        IdCode = c.String(),
                        StockStandardName = c.String(),
                        SolventUsed = c.String(),
                        SolventSupplierName = c.String(),
                        Purity = c.Double(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.StockStandardId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitId = c.Int(nullable: false, identity: true),
                        UnitShorthandName = c.String(nullable: false),
                        UnitFullName = c.String(),
                        UnitType = c.String(),
                    })
                .PrimaryKey(t => t.UnitId);
            
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
                "dbo.MaxxamMadeReagents",
                c => new
                    {
                        MaxxamMadeReagentId = c.Int(nullable: false, identity: true),
                        MaxxamId = c.String(),
                        IdCode = c.String(),
                        MaxxamMadeReagentName = c.String(),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.MaxxamMadeReagentId);
            
            CreateTable(
                "dbo.MaxxamMadeStandards",
                c => new
                    {
                        MaxxamMadeStandardId = c.Int(nullable: false, identity: true),
                        MaxxamId = c.String(),
                        IdCode = c.String(),
                        MaxxamMadeStandardName = c.String(),
                        SolventUsed = c.String(),
                        SolventSupplierName = c.String(),
                        Purity = c.Double(nullable: false),
                        LastModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.MaxxamMadeStandardId);
            
            CreateTable(
                "dbo.MSDS",
                c => new
                    {
                        MSDSId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        DateAdded = c.DateTime(nullable: false),
                        MSDSNotes = c.String(),
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
            DropForeignKey("dbo.SurrogateSpikingStandards", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropForeignKey("dbo.SurrogateSpikingStandards", "SyringeId_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.InventoryItems", "Supplier_SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SpikingStandards", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropForeignKey("dbo.SpikingStandards", "SyringeId_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.InventoryItems", "SecondDeviceUsed_DeviceId", "dbo.Devices");
            DropForeignKey("dbo.MSDS", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropForeignKey("dbo.InventoryItems", "MaxxamMadeStandard_MaxxamMadeStandardId", "dbo.MaxxamMadeStandards");
            DropForeignKey("dbo.InventoryItems", "MaxxamMadeReagent_MaxxamMadeReagentId", "dbo.MaxxamMadeReagents");
            DropForeignKey("dbo.Locations", "InventoryItem_InventoryItemId", "dbo.InventoryItems");
            DropForeignKey("dbo.IntermediateStandards", "PrepList_PrepListId", "dbo.PrepLists");
            DropForeignKey("dbo.WorkingStandards", "PrepList_PrepListId", "dbo.PrepLists");
            DropForeignKey("dbo.PrepListItems", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.InventoryItems", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.PrepListItems", "StockStandard_StockStandardId", "dbo.StockStandards");
            DropForeignKey("dbo.InventoryItems", "StockStandard_StockStandardId", "dbo.StockStandards");
            DropForeignKey("dbo.PrepListItems", "StockReagent_ReagentId", "dbo.StockReagents");
            DropForeignKey("dbo.InventoryItems", "StockReagent_ReagentId", "dbo.StockReagents");
            DropForeignKey("dbo.PrepListItems", "PrepList_PrepListId", "dbo.PrepLists");
            DropForeignKey("dbo.PrepListItems", "IntermediateStandard_IntermediateStandardId", "dbo.IntermediateStandards");
            DropForeignKey("dbo.InventoryItems", "IntermediateStandard_IntermediateStandardId", "dbo.IntermediateStandards");
            DropForeignKey("dbo.InventoryItems", "FirstDeviceUsed_DeviceId", "dbo.Devices");
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
            DropIndex("dbo.SurrogateSpikingStandards", new[] { "InventoryItem_InventoryItemId" });
            DropIndex("dbo.SurrogateSpikingStandards", new[] { "SyringeId_DeviceId" });
            DropIndex("dbo.SpikingStandards", new[] { "InventoryItem_InventoryItemId" });
            DropIndex("dbo.SpikingStandards", new[] { "SyringeId_DeviceId" });
            DropIndex("dbo.MSDS", new[] { "InventoryItem_InventoryItemId" });
            DropIndex("dbo.WorkingStandards", new[] { "PrepList_PrepListId" });
            DropIndex("dbo.PrepListItems", new[] { "Unit_UnitId" });
            DropIndex("dbo.PrepListItems", new[] { "StockStandard_StockStandardId" });
            DropIndex("dbo.PrepListItems", new[] { "StockReagent_ReagentId" });
            DropIndex("dbo.PrepListItems", new[] { "PrepList_PrepListId" });
            DropIndex("dbo.PrepListItems", new[] { "IntermediateStandard_IntermediateStandardId" });
            DropIndex("dbo.IntermediateStandards", new[] { "PrepList_PrepListId" });
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
            DropIndex("dbo.InventoryItems", new[] { "Supplier_SupplierId" });
            DropIndex("dbo.InventoryItems", new[] { "SecondDeviceUsed_DeviceId" });
            DropIndex("dbo.InventoryItems", new[] { "MaxxamMadeStandard_MaxxamMadeStandardId" });
            DropIndex("dbo.InventoryItems", new[] { "MaxxamMadeReagent_MaxxamMadeReagentId" });
            DropIndex("dbo.InventoryItems", new[] { "Unit_UnitId" });
            DropIndex("dbo.InventoryItems", new[] { "StockStandard_StockStandardId" });
            DropIndex("dbo.InventoryItems", new[] { "StockReagent_ReagentId" });
            DropIndex("dbo.InventoryItems", new[] { "IntermediateStandard_IntermediateStandardId" });
            DropIndex("dbo.InventoryItems", new[] { "FirstDeviceUsed_DeviceId" });
            DropIndex("dbo.InventoryItems", new[] { "InventoryLocation_InventoryLocationId" });
            DropIndex("dbo.InventoryItems", new[] { "Department_DepartmentId" });
            DropIndex("dbo.CertificateOfAnalysis", new[] { "InventoryItem_InventoryItemId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SurrogateSpikingStandards");
            DropTable("dbo.Suppliers");
            DropTable("dbo.SpikingStandards");
            DropTable("dbo.MSDS");
            DropTable("dbo.MaxxamMadeStandards");
            DropTable("dbo.MaxxamMadeReagents");
            DropTable("dbo.WorkingStandards");
            DropTable("dbo.Units");
            DropTable("dbo.StockStandards");
            DropTable("dbo.StockReagents");
            DropTable("dbo.PrepListItems");
            DropTable("dbo.PrepLists");
            DropTable("dbo.IntermediateStandards");
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
