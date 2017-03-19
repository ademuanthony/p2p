namespace Peer2peer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonationPackageId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "PackageId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donations", "PackageId");
        }
    }
}
