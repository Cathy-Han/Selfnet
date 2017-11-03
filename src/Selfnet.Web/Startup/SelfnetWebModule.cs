using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Selfnet.Configuration;
using Selfnet.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Selfnet.Web.Startup
{
    [DependsOn(
        typeof(SelfnetApplicationModule), 
        typeof(SelfnetEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class SelfnetWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public SelfnetWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(SelfnetConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<SelfnetNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(SelfnetApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SelfnetWebModule).GetAssembly());
        }
    }
}