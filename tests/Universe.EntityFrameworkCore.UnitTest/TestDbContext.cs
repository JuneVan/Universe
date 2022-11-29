namespace Universe.EntityFrameworkCore.UnitTest
{
    public class TestDbContext : EfCoreDbContext<TestDbContext>
    {
        public TestDbContext(DbContextOptions options, IServiceProvider serviceProvider)
            : base(options, serviceProvider)
        {

        }
        public DbSet<EntityA>? EntityAs { get; set; }
        public DbSet<EntityB>? EntityBs { get; set; }
         
    }
}
