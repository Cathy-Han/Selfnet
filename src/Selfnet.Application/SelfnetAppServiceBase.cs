using Abp.Application.Services;

namespace Selfnet
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class SelfnetAppServiceBase : ApplicationService
    {
        protected SelfnetAppServiceBase()
        {
            LocalizationSourceName = SelfnetConsts.LocalizationSourceName;
        }
    }
}