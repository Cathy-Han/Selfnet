using System;
using System.Threading.Tasks;
using Abp.TestBase;
using Selfnet.EntityFrameworkCore;
using Selfnet.Tests.TestDatas;

namespace Selfnet.Tests
{
    public class SelfnetTestBase : AbpIntegratedTestBase<SelfnetTestModule>
    {
        public SelfnetTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<SelfnetDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<SelfnetDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<SelfnetDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SelfnetDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<SelfnetDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<SelfnetDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<SelfnetDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<SelfnetDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
