using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Penna.Data.Configurations;
using Penna.Entities.Models;

namespace Penna.Data.EntityFramework.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInOut> ProductInOuts { get; set; }
        public DbSet<Labor> Labors { get; set; }
        public DbSet<CurrentAccount> CurrentAccounts { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ProjectFile> ProjectFiles { get; set; }
        public DbSet<BlockTeam> BlockTeams { get; set; }
        public DbSet<BlockSubcontractor> BlockSubcontractors { get; set; }
        public DbSet<BlockSubcontractorBusinessGroup> BlockSubcontractorBusinessGroups { get; set; }
        public DbSet<CurrentAccountDetail> CurrentAccountDetails { get; set; }
        public DbSet<Authority> Authorities { get; set; }
        public DbSet<AuthorityGroup> AuthorityGroups { get; set; }
        public DbSet<UserPosition> UserPositions { get; set; }
        public DbSet<UserPositionAuthority> UserPositionAuthorities { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<FixtureEmbezzled> FixtureEmbezzleds { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseTender> PurchaseTenders { get; set; }
        public DbSet<ConcreteRequest> ConcreteRequests { get; set; }
        public DbSet<ConcreteCasting> ConcreteCastings { get; set; }
        public DbSet<WorkPlan> WorkPlans { get; set; }
        public DbSet<WorkPlanDetail> WorkPlanDetails { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Users
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            // Tanımlamalar
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new TownConfiguration());
            modelBuilder.ApplyConfiguration(new UnitConfiguration());
            // Genel Tablolar
            modelBuilder.ApplyConfiguration(new TenantConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductInOutConfiguration());
            modelBuilder.ApplyConfiguration(new LaborConfiguration());
            modelBuilder.ApplyConfiguration(new CurrentAccountConfiguration());
            modelBuilder.ApplyConfiguration(new BlockConfiguration());
            modelBuilder.ApplyConfiguration(new ApartmentConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectFileConfiguration());
            modelBuilder.ApplyConfiguration(new BlockTeamConfiguration());
            modelBuilder.ApplyConfiguration(new BlockSubcontractorConfiguration());
            modelBuilder.ApplyConfiguration(new BlockSubcontractorBusinessGroupConfiguration());
            modelBuilder.ApplyConfiguration(new CurrentAccountDetailConfiguration());
            modelBuilder.ApplyConfiguration(new AthorityConfiguration());
            modelBuilder.ApplyConfiguration(new AthoritGroupConfiguration());
            modelBuilder.ApplyConfiguration(new UserPositionConfiguration());
            modelBuilder.ApplyConfiguration(new UserPositionAuthorityConfiguration());
            modelBuilder.ApplyConfiguration(new FixtureConfiguration());
            modelBuilder.ApplyConfiguration(new FixtureEmbezziedConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseRequestConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseTenderConfiguration());
            modelBuilder.ApplyConfiguration(new ConcreteRequestConfiguration());
            modelBuilder.ApplyConfiguration(new ConcreteCastingConfiguration());
            modelBuilder.ApplyConfiguration(new WorkPlanConfiguration());
            modelBuilder.ApplyConfiguration(new WorkPlanDetailConfiguration());
        }
    }
}
