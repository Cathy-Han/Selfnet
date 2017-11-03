using System.Reflection;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Selfnet.Web.Startup;

namespace Selfnet.Web.Tests
{
    [DependsOn(
        typeof(SelfnetWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class SelfnetWebTestModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SelfnetWebTestModule).GetAssembly());
        }
    }
}