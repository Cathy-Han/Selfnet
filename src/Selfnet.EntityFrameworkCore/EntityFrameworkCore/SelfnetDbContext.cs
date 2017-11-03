using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Selfnet.EntityFrameworkCore
{
    public class SelfnetDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public SelfnetDbContext(DbContextOptions<SelfnetDbContext> options) 
            : base(options)
        {

        }
    }
}
