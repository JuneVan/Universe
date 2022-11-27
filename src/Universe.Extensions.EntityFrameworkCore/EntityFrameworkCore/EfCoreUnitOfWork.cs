namespace Universe.Extensions.EntityFrameworkCore
{
    public class EfCoreUnitOfWork<TDbContext> : IUnitOfWork
          where TDbContext : EfCoreDbContext
    {
        private readonly IDbContextTransaction _dbContextTransaction;
        private readonly TDbContext _context;
        private readonly ISignal _signal;
        public EfCoreUnitOfWork(TDbContext context,
            ISignal signal)
        {
            _context = context;
            _signal = signal;
            _dbContextTransaction = _context.Database.BeginTransaction();
        }
        public void RegisterNew<TEntity,TKey>(TEntity entity)
          where TEntity : class, IEntity<TKey>
        {
            _context.Set<TEntity>().Add(entity);
        }
        public void RegisterModified<TEntity, TKey>(TEntity entity)
            where TEntity : class, IEntity<TKey>
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void RegisterDeleted<TEntity, TKey>(TEntity entity)
            where TEntity : class, IEntity<TKey>
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }
        public async Task<int> CommitAsync()
        {
            int result;
            try
            {
                result = await _context.SaveChangesAsync();
                await _dbContextTransaction.CommitAsync();
            }
            catch
            {
                _dbContextTransaction.Rollback();
                throw;
            }
            return result;
        }

        public void Dispose()
        {
            if (_dbContextTransaction != null)
                _dbContextTransaction.Dispose();
            if (_context != null)
                _context.Dispose();
        }
    }
}
