using Abp.Modules;
using Abp.Reflection.Extensions;
using Selfnet.Localization;

namespace Selfnet
{
    public class SelfnetCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            SelfnetLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SelfnetCoreModule).GetAssembly());
        }
    }
}