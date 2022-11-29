using Microsoft.Extensions.Logging;

namespace Universe.EntityFrameworkCore.UnitTest
{
    public abstract class UnitTestBase
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddLogging(logger =>
            {
                logger.ClearProviders();
                logger.AddDebug();
            });
            services.AddUniverse(AppDomain.CurrentDomain.GetAssemblies())
                .AddEntityFrameworkCore<TestDbContext>(configure =>
                {
                    configure.UseNpgsql("Host=127.0.0.1;Port=5432;Database=TestDb;UserName=postgres;Password=123456;"); 
                });

            ServiceProvider = services.BuildServiceProvider();
        } 
    }
}