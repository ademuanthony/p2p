namespace Peer2peer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketMAp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "Donation_Id", "dbo.Donations");
            DropForeignKey("dbo.Donations", "Id", "dbo.Packages");
            DropIndex("dbo.Donations", new[] { "Id" });
            DropIndex("dbo.Packages", new[] { "Donation_Id" });
            DropPrimaryKey("dbo.Donations");
            AlterColumn("dbo.Donations", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Donations", "Id");
            DropColumn("dbo.Packages", "Donation_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "Donation_Id", c => c.Int());
            DropPrimaryKey("dbo.Donations");
            AlterColumn("dbo.Donations", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Donations", "Id");
            CreateIndex("dbo.Packages", "Donation_Id");
            CreateIndex("dbo.Donations", "Id");
            AddForeignKey("dbo.Donations", "Id", "dbo.Packages", "Id");
            AddForeignKey("dbo.Packages", "Donation_Id", "dbo.Donations", "Id");
        }
    }
}
