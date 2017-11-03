using Selfnet.EntityFrameworkCore;

namespace Selfnet.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly SelfnetDbContext _context;

        public TestDataBuilder(SelfnetDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}