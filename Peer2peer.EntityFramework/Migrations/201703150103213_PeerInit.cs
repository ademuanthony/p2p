namespace Peer2peer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PeerInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromUserId = c.Int(nullable: false),
                        ToUserId = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IntergrityScores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Source = c.String(),
                        Score = c.Int(nullable: false),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Status = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ExpectedRi = c.Double(nullable: false),
                        CurrentRi = c.Double(nullable: false),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PackageTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.User_Id)
                .Index(t => t.TypeId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PackageTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Ri = c.Double(nullable: false),
                        LifeSpan = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Referrals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        DownlineId = c.Int(nullable: false),
                        Downline_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.Downline_Id)
                .ForeignKey("dbo.AbpUsers", t => t.User_Id)
                .Index(t => t.Downline_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.AbpUsers", "ProfileImage", c => c.String());
            AddColumn("dbo.AbpUsers", "BankName", c => c.String());
            AddColumn("dbo.AbpUsers", "AccountName", c => c.String());
            AddColumn("dbo.AbpUsers", "AccountNumber", c => c.String());
            AddColumn("dbo.AbpUsers", "Score", c => c.String());
            AddColumn("dbo.AbpUsers", "Status", c => c.String());
            AddColumn("dbo.AbpUsers", "Circel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Referrals", "User_Id", "dbo.AbpUsers");
            DropForeignKey("dbo.Referrals", "Downline_Id", "dbo.AbpUsers");
            DropForeignKey("dbo.Packages", "User_Id", "dbo.AbpUsers");
            DropForeignKey("dbo.Packages", "TypeId", "dbo.PackageTypes");
            DropForeignKey("dbo.IntergrityScores", "User_Id", "dbo.AbpUsers");
            DropIndex("dbo.Referrals", new[] { "User_Id" });
            DropIndex("dbo.Referrals", new[] { "Downline_Id" });
            DropIndex("dbo.Packages", new[] { "User_Id" });
            DropIndex("dbo.Packages", new[] { "TypeId" });
            DropIndex("dbo.IntergrityScores", new[] { "User_Id" });
            DropColumn("dbo.AbpUsers", "Circel");
            DropColumn("dbo.AbpUsers", "Status");
            DropColumn("dbo.AbpUsers", "Score");
            DropColumn("dbo.AbpUsers", "AccountNumber");
            DropColumn("dbo.AbpUsers", "AccountName");
            DropColumn("dbo.AbpUsers", "BankName");
            DropColumn("dbo.AbpUsers", "ProfileImage");
            DropTable("dbo.Referrals");
            DropTable("dbo.PackageTypes");
            DropTable("dbo.Packages");
            DropTable("dbo.IntergrityScores");
            DropTable("dbo.Donations");
        }
    }
}
