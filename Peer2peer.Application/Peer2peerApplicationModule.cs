using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace Peer2peer
{
    [DependsOn(typeof(Peer2peerCoreModule), typeof(AbpAutoMapperModule))]
    public class Peer2peerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
