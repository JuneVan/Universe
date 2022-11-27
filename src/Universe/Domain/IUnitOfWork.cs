namespace Universe.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void RegisterNew<TEntity, TKey>(TEntity entity)
                    where TEntity : class, IEntity<TKey>; 
        void RegisterModified<TEntity, TKey>(TEntity entity)
           where TEntity : class, IEntity<TKey>; 
        void RegisterDeleted<TEntity, TKey>(TEntity entity)
           where TEntity : class, IEntity<TKey>;
        Task<int> CommitAsync();
    }
}
