using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Peer2peer.EntityFramework;

namespace Peer2peer
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(Peer2peerCoreModule))]
    public class Peer2peerDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Peer2PeerDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
