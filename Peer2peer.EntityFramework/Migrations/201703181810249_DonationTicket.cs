namespace Peer2peer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DonationTicket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DonationTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        PackageId = c.Int(nullable: false),
                        DonationId = c.Int(),
                        DateCreated = c.DateTime(nullable: false),
                        Status = c.String(),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId);
            
            AddColumn("dbo.Donations", "TicketId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DonationTickets", "PackageId", "dbo.Packages");
            DropIndex("dbo.DonationTickets", new[] { "PackageId" });
            DropColumn("dbo.Donations", "TicketId");
            DropTable("dbo.DonationTickets");
        }
    }
}
