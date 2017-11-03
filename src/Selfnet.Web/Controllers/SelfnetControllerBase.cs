using Abp.AspNetCore.Mvc.Controllers;

namespace Selfnet.Web.Controllers
{
    public abstract class SelfnetControllerBase: AbpController
    {
        protected SelfnetControllerBase()
        {
            LocalizationSourceName = SelfnetConsts.LocalizationSourceName;
        }
    }
}