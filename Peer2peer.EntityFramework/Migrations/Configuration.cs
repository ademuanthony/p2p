using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using Peer2peer.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace Peer2peer.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<Peer2peer.EntityFramework.Peer2PeerDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Peer2peer";
        }

        protected override void Seed(Peer2peer.EntityFramework.Peer2PeerDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
