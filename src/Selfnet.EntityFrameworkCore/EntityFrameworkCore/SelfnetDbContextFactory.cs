using Selfnet.Configuration;
using Selfnet.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Selfnet.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class SelfnetDbContextFactory : IDesignTimeDbContextFactory<SelfnetDbContext>
    {
        public SelfnetDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SelfnetDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(SelfnetConsts.ConnectionStringName)
            );

            return new SelfnetDbContext(builder.Options);
        }
    }
}