using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Peer2peer.EntityFramework;

namespace Peer2peer.Migrator
{
    [DependsOn(typeof(Peer2peerDataModule))]
    public class Peer2peerMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<Peer2PeerDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}