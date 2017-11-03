using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Selfnet.EntityFrameworkCore
{
    [DependsOn(
        typeof(SelfnetCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class SelfnetEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SelfnetEntityFrameworkCoreModule).GetAssembly());
        }
    }
}