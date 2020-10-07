namespace koinfast.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deposits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefNo = c.String(unicode: false),
                        ProofUrl = c.String(unicode: false),
                        DepositDate = c.DateTime(nullable: false, precision: 0),
                        ApprovalDate = c.DateTime(nullable: false, precision: 0),
                        Investor_Id = c.Int(),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Investors", t => t.Investor_Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Investor_Id)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.Investments",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        InvestmentNo = c.String(unicode: false),
                        StartDate = c.DateTime(precision: 0),
                        EndDate = c.DateTime(precision: 0),
                        Amount = c.Double(nullable: false),
                        Payback = c.Double(nullable: false),
                        Percentage = c.Double(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Deposits", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Investors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.Int(nullable: false),
                        Name = c.String(unicode: false),
                        Surname = c.String(unicode: false),
                        Phone = c.String(unicode: false),
                        Country = c.String(unicode: false),
                        City = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        AccNo = c.String(unicode: false),
                        Bank = c.String(unicode: false),
                        InvestorNo = c.String(unicode: false),
                        TotalSponsees = c.Int(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                        SponsorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Investors", t => t.SponsorId)
                .Index(t => t.SponsorId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefNo = c.String(unicode: false),
                        TransactionType = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        TransactionDate = c.DateTime(nullable: false, precision: 0),
                        Investor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Investors", t => t.Investor_Id)
                .Index(t => t.Investor_Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        PackageType = c.Int(nullable: false),
                        Return = c.Double(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Deposits", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.Transactions", "Investor_Id", "dbo.Investors");
            DropForeignKey("dbo.Investors", "SponsorId", "dbo.Investors");
            DropForeignKey("dbo.Deposits", "Investor_Id", "dbo.Investors");
            DropForeignKey("dbo.Investments", "Id", "dbo.Deposits");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Transactions", new[] { "Investor_Id" });
            DropIndex("dbo.Investors", new[] { "SponsorId" });
            DropIndex("dbo.Investments", new[] { "Id" });
            DropIndex("dbo.Deposits", new[] { "Package_Id" });
            DropIndex("dbo.Deposits", new[] { "Investor_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Packages");
            DropTable("dbo.Transactions");
            DropTable("dbo.Investors");
            DropTable("dbo.Investments");
            DropTable("dbo.Deposits");
        }
    }
}
