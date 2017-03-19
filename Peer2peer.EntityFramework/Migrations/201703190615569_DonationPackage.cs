namespace Peer2peer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonationPackage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Donations", "Id", "dbo.Packages");
            AddColumn("dbo.Packages", "Donation_Id", c => c.Int());
            CreateIndex("dbo.Packages", "Donation_Id");
            AddForeignKey("dbo.Packages", "Donation_Id", "dbo.Donations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Packages", "Donation_Id", "dbo.Donations");
            DropIndex("dbo.Packages", new[] { "Donation_Id" });
            DropColumn("dbo.Packages", "Donation_Id");
            AddForeignKey("dbo.Donations", "Id", "dbo.Packages", "Id");
        }
    }
}
