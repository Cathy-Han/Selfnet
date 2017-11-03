using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Selfnet
{
    [DependsOn(
        typeof(SelfnetCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SelfnetApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SelfnetApplicationModule).GetAssembly());
        }
    }
}