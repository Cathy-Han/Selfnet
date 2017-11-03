using Microsoft.EntityFrameworkCore;

namespace Selfnet.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<SelfnetDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for SelfnetDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
