namespace Peer2peer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackagePrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PackageTypes", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.PackageTypes", "ReturnOnInvestment", c => c.Double(nullable: false));
            DropColumn("dbo.PackageTypes", "Ri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PackageTypes", "Ri", c => c.Double(nullable: false));
            DropColumn("dbo.PackageTypes", "ReturnOnInvestment");
            DropColumn("dbo.PackageTypes", "Price");
        }
    }
}
