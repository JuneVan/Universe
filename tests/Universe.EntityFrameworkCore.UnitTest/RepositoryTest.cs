namespace Universe.EntityFrameworkCore.UnitTest
{
    public class RepositoryTest : UnitTestBase
    {
        [Test]
        public void Resolve()
        {
            var entityARepository = ServiceProvider.GetRequiredService<IRepository<EntityA>>();
            Assert.IsNotNull(entityARepository);
        }

        [Test]
        public void Insert()
        {
            var entityA = new EntityA()
            {
                Id = 1,
                Name = "A"
            };
            var entityARepository = ServiceProvider.GetRequiredService<IRepository<EntityA>>();
            entityARepository.InsertAsync(entityA).Wait();
            entityARepository.UnitOfWork.CommitAsync().Wait();
            Assert.Pass("aa");
        }
        [Test]
        public void InsertWithTransactionError()
        {
            var entityARepository = ServiceProvider.GetRequiredService<IRepository<EntityA>>();
            var entityBRepository = ServiceProvider.GetRequiredService<IRepository<EntityB>>();

            var entityA = new EntityA()
            {
                Name = "A" + new Random().Next(1000, 9999)
            };

            var entityAId = entityARepository.InsertAndGetIdAsync(entityA).Result;
            var entityB = new EntityB()
            {
                Name = "ABCDEFGHIJLMNOPQRSTUVWXYZABCDEFGHIJLMNOPQRSTUVWXYZ",
                EntityAId = entityAId
            };
            entityBRepository.InsertAsync(entityB).Wait();
            entityARepository.UnitOfWork.CommitAsync().Wait();
            Assert.Pass("aa");
        }
        [Test]
        public void InsertWithTransaction()
        {
            var entityARepository = ServiceProvider.GetRequiredService<IRepository<EntityA>>();
            var entityBRepository = ServiceProvider.GetRequiredService<IRepository<EntityB>>();

            var entityA = new EntityA()
            {
                Name = "A" + new Random().Next(1000, 9999)
            };

            var entityAId = entityARepository.InsertAndGetIdAsync(entityA).Result;
            var entityB = new EntityB()
            {
                Name = "ABCDEFGHIJLMNOPQRSTUVWXYZ",
                EntityAId = entityAId
            };
            entityBRepository.InsertAsync(entityB).Wait();
            entityARepository.UnitOfWork.CommitAsync().Wait();
            Assert.Pass("aa");
        }
    }
}
