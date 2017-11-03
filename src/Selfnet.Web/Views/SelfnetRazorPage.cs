using Abp.AspNetCore.Mvc.Views;

namespace Selfnet.Web.Views
{
    public abstract class SelfnetRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected SelfnetRazorPage()
        {
            LocalizationSourceName = SelfnetConsts.LocalizationSourceName;
        }
    }
}
