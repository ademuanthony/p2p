namespace Peer2peer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RewardTicket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReferralRewardDonations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                        FromUserId = c.Long(nullable: false),
                        ToUserId = c.Long(),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.ToUserId)
                .Index(t => t.ToUserId);
            
            CreateTable(
                "dbo.ReferralRewardTickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        DonationId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReferralRewardDonations", "ToUserId", "dbo.AbpUsers");
            DropIndex("dbo.ReferralRewardDonations", new[] { "ToUserId" });
            DropTable("dbo.ReferralRewardTickets");
            DropTable("dbo.ReferralRewardDonations");
        }
    }
}
