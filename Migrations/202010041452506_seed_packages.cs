namespace koinfast.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed_packages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "Price");
        }
    }
}
