namespace Peer2peer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NavigationPpts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "User_Id", "dbo.AbpUsers");
            DropForeignKey("dbo.Referrals", "Downline_Id", "dbo.AbpUsers");
            DropIndex("dbo.Packages", new[] { "User_Id" });
            DropIndex("dbo.Referrals", new[] { "Downline_Id" });
            DropIndex("dbo.Referrals", new[] { "User_Id" });
            DropColumn("dbo.Packages", "UserId");
            DropColumn("dbo.Referrals", "DownlineId");
            RenameColumn(table: "dbo.Packages", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Referrals", name: "Downline_Id", newName: "DownlineId");
            DropPrimaryKey("dbo.Donations");
            AddColumn("dbo.Donations", "User_Id", c => c.Long());
            AlterColumn("dbo.Donations", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Donations", "FromUserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Donations", "ToUserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Packages", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Packages", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Referrals", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Referrals", "DownlineId", c => c.Long(nullable: false));
            AlterColumn("dbo.Referrals", "DownlineId", c => c.Long(nullable: false));
            AlterColumn("dbo.Referrals", "User_Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Donations", "Id");
            CreateIndex("dbo.Donations", "Id");
            CreateIndex("dbo.Donations", "ToUserId");
            CreateIndex("dbo.Donations", "User_Id");
            CreateIndex("dbo.Packages", "UserId");
            CreateIndex("dbo.Referrals", "DownlineId");
            CreateIndex("dbo.Referrals", "User_Id");
            AddForeignKey("dbo.Donations", "User_Id", "dbo.AbpUsers", "Id");
            AddForeignKey("dbo.Donations", "Id", "dbo.Packages", "Id");
            AddForeignKey("dbo.Donations", "ToUserId", "dbo.AbpUsers", "Id");
            AddForeignKey("dbo.Packages", "UserId", "dbo.AbpUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Referrals", "DownlineId", "dbo.AbpUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Donations", "PackageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Donations", "PackageId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Referrals", "DownlineId", "dbo.AbpUsers");
            DropForeignKey("dbo.Packages", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Donations", "ToUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Donations", "Id", "dbo.Packages");
            DropForeignKey("dbo.Donations", "User_Id", "dbo.AbpUsers");
            DropIndex("dbo.Referrals", new[] { "User_Id" });
            DropIndex("dbo.Referrals", new[] { "DownlineId" });
            DropIndex("dbo.Packages", new[] { "UserId" });
            DropIndex("dbo.Donations", new[] { "User_Id" });
            DropIndex("dbo.Donations", new[] { "ToUserId" });
            DropIndex("dbo.Donations", new[] { "Id" });
            DropPrimaryKey("dbo.Donations");
            AlterColumn("dbo.Referrals", "User_Id", c => c.Long());
            AlterColumn("dbo.Referrals", "DownlineId", c => c.Long());
            AlterColumn("dbo.Referrals", "DownlineId", c => c.Int(nullable: false));
            AlterColumn("dbo.Referrals", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Packages", "UserId", c => c.Long());
            AlterColumn("dbo.Packages", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Donations", "ToUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Donations", "FromUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Donations", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Donations", "User_Id");
            AddPrimaryKey("dbo.Donations", "Id");
            RenameColumn(table: "dbo.Referrals", name: "DownlineId", newName: "Downline_Id");
            RenameColumn(table: "dbo.Packages", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Referrals", "DownlineId", c => c.Int(nullable: false));
            AddColumn("dbo.Packages", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Referrals", "User_Id");
            CreateIndex("dbo.Referrals", "Downline_Id");
            CreateIndex("dbo.Packages", "User_Id");
            AddForeignKey("dbo.Referrals", "Downline_Id", "dbo.AbpUsers", "Id");
            AddForeignKey("dbo.Packages", "User_Id", "dbo.AbpUsers", "Id");
        }
    }
}
