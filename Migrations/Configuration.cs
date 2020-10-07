using koinfast.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace koinfast.Migrations
{

  internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
      AutomaticMigrationDataLossAllowed = true;

    }

    protected override void Seed(ApplicationDbContext appDbContext)
    {

      var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDbContext));

      var user = new ApplicationUser
      {
        UserName = "admin@koinfast.com",
        Email = "admin@koinfast.com",
        EmailConfirmed = true,
        SecurityStamp = new Guid().ToString()
      };

      appDbContext.Packages.AddOrUpdate(p => p.Name,
        new Package { Name = "PACKAGE 1", PackageType = PackageType.Shares, Duration = 15, Price = 0, Return = 0 },
        new Package { Name = "PACKAGE 2", PackageType = PackageType.Shares, Duration = 30, Price = 0, Return = 0 },
        new Package { Name = "ROOKIE", PackageType = PackageType.Shares, Duration = 365, Price = 60, Return = 1500 },
        new Package { Name = "ROOKIE+", PackageType = PackageType.Shares, Duration = 365, Price = 120, Return = 3000 },
        new Package { Name = "BASIC", PackageType = PackageType.Shares, Duration = 365, Price = 220, Return = 5000 },
        new Package { Name = "STARTER", PackageType = PackageType.Shares, Duration = 365, Price = 550, Return = 12000 },
        new Package { Name = "EXECUTIVE", PackageType = PackageType.Shares, Duration = 365, Price = 1000, Return = 20000 },
        new Package { Name = "EXECUTIVE+", PackageType = PackageType.Shares, Duration = 365, Price = 1500, Return = 25000 },
        new Package { Name = "PREMIUM", PackageType = PackageType.Shares, Duration = 365, Price = 3000, Return = 50000 },
        new Package { Name = "PROFESSIONAL", PackageType = PackageType.Shares, Duration = 365, Price = 5000, Return = 75000 },
        new Package { Name = "ELITE", PackageType = PackageType.Shares, Duration = 365, Price = 10000, Return = 150000 }
       );

      appDbContext.Roles.AddOrUpdate(r => r.Name,
        new IdentityRole { Name = "Admin" },
        new IdentityRole { Name = "Investor" }
      );

      appDbContext.SaveChanges();

      if (userManager.Create(user, "Koinfast@2018").Succeeded)
      {
        userManager.AddToRole(user.Id, "Admin");
      }
      base.Seed(appDbContext);
    }
  }
}
