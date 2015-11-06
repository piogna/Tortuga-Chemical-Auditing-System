using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;

namespace TMNT.Models {
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser {
        public DateTime? LastPasswordChange { get; set; }
        public DateTime? NextRequiredPasswordChange { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<UserSettings> UserSettings { get; set; }
        public virtual ICollection<DeviceVerification> DeviceVerifications { get; set; }

        //foreign keys
        //[Required]
        public virtual Department Department { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceTest> DeviceTests { get; set; }
        public DbSet<DeviceVerification> DeviceVerifications { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<InventoryLocation> InventoryLocations { get; set; }
        public DbSet<StockReagent> StockReagents { get; set; }
        public DbSet<StockStandard> StockStandards { get; set; }
        public DbSet<IntermediateStandard> IntermediateStandards { get; set; }
        public DbSet<WorkingStandard> WorkingStandards { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<PrepList> PrepLists { get; set; }
        public DbSet<PrepListItem> PrepListItems { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<CertificateOfAnalysis> CertificatesOfAnalysis { get; set; }
        public DbSet<MSDS> MSDS { get; set; }
        public DbSet<PreparedReagent> PreparedReagent { get; set; }
        public DbSet<PreparedStandard> PreparedStandard { get; set; }
        public DbSet<Feedback> Feedback { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceVerification>()
            .HasMany(dv => dv.DeviceTests)
            .WithOptional()
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
            .HasMany(dv => dv.DeviceVerifications)
            .WithOptional()
            .WillCascadeOnDelete(false);
        }
    }
}