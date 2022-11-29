using Microsoft.EntityFrameworkCore.Design;

namespace Universe.EntityFrameworkCore.UnitTest
{
    public class TestDbFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            ServiceCollection service = new ServiceCollection();
            DbContextOptionsBuilder<TestDbContext> optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=TestDb;UserName=postgres;Password=123456;");
            return new TestDbContext(optionsBuilder.Options, service.BuildServiceProvider());
        }
    }
}
