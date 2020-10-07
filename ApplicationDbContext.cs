
using koinfast.Migrations;
using koinfast.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace koinfast
{

  public class ApplicationUser : IdentityUser
  {
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      return userIdentity;
    }
  }

  [DbConfigurationType(typeof(MySqlEFConfiguration))]
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext() : base("koinfastCon", throwIfV1Schema: false)
    {
      Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
    }

    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }
    public DbSet<Investor> Investors { get; set; }
    public DbSet<Deposit> Deposits { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Investment> Investments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Investor>()
        .HasOptional(i => i.Sponsor)
        .WithMany(s => s.Sponsees)
        .HasForeignKey(i => i.SponsorId);

      modelBuilder.Entity<Deposit>()
        .HasOptional(p => p.Investment)
        .WithRequired(i => i.Deposit);

    }

  }
}