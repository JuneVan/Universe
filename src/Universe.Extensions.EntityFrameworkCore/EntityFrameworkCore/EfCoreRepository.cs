namespace Universe.Extensions.EntityFrameworkCore
{
    public class EfCoreRepository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
       where TEntity : class, IEntity<TKey>
        where TDbContext : DbContext
    {
        private bool disposedValue;
        private readonly ISignal _signal;
        public EfCoreRepository(IUnitOfWork unitOfWork,
            TDbContext context,
            ISignal signal)
        {
            Context = context;
            UnitOfWork = unitOfWork;
            _signal = signal;
        }
        protected TDbContext Context { get; }
        public IUnitOfWork UnitOfWork { get; }
        protected CancellationToken CancellationToken => _signal.Token;
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            UnitOfWork.RegisterNew<TEntity, TKey>(entity);
            await Task.CompletedTask;
            return entity;
        }
        public async Task<TKey> InsertAndGetIdAsync(TEntity entity)
        {
            await InsertAsync(entity);
            await Context.SaveChangesAsync(CancellationToken);
            return entity.Id;
        }
        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            if (EqualityComparer<TKey>.Default.Equals(entity.Id, default(TKey)))
            {
               return await InsertAsync(entity);
            }
            else
            {
              return  await UpdateAsync(entity);
            }
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            UnitOfWork.RegisterModified<TEntity,TKey>(entity);
            await Task.CompletedTask;
            return entity;
        }
        public async Task DeleteAsync(TEntity entity)
        {
            UnitOfWork.RegisterDeleted<TEntity, TKey>(entity);
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(TKey id)
        {
            var entry = Context.ChangeTracker.Entries()
               .FirstOrDefault(
                   entry =>
                       entry.Entity is TEntity &&
                       EqualityComparer<TKey>.Default.Equals(id, ((TEntity)entry.Entity).Id)
               );

            if (entry?.Entity is not TEntity entity)
                return;
            await DeleteAsync(entity);
        }
        public virtual async Task<TEntity?> FirstOrDefaultAsync(TKey id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(CreateEqualityExpressionForId(id), CancellationToken);
        }
        public virtual async Task<TEntity?> IncludingFirstOrDefaultAsync(TKey id, params Expression<Func<TEntity, object>>[] propertySelectors)
        {

            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            if (propertySelectors != null)
            {
                foreach (Expression<Func<TEntity, object>> propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return await query.FirstOrDefaultAsync(CreateEqualityExpressionForId(id), CancellationToken);
        }
        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TKey id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");
            object? idValue = Convert.ChangeType(id, typeof(TKey));

            Expression<Func<object>> closure = () => idValue;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {

            return await Context.Set<TEntity>().FirstOrDefaultAsync(expression, CancellationToken);
        }

        public async Task<TEntity?> IncludingFirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] propertySelectors)
        {

            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            if (propertySelectors != null)
            {
                foreach (Expression<Func<TEntity, object>> propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return await query.FirstOrDefaultAsync(expression, CancellationToken);
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {

            return await Context.Set<TEntity>().CountAsync(expression, CancellationToken);
        }
        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            if (propertySelectors != null)
            {
                foreach (Expression<Func<TEntity, object>> propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }
            return query;
        }
        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll()
                .ToListAsync(CancellationToken);
        }
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await GetAll()
                .Where(expression)
                .ToListAsync(CancellationToken);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public class EfCoreRepository<TDbContext, TEntity> : EfCoreRepository<TDbContext, TEntity, long>, IRepository<TEntity>
        where TEntity : class, IEntity<long>
        where TDbContext : DbContext
    {
        public EfCoreRepository(IUnitOfWork unitOfWork,
            TDbContext context,
            ISignal signal)
            : base(unitOfWork, context, signal)
        {
        }
    }
}