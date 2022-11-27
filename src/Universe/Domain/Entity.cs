namespace Universe.Domain
{
    /// <summary>
    /// 实体泛型基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class Entity<TKey> : IEntity<TKey>
    {
#pragma warning disable CS8618 
        public virtual TKey Id { get; set; }
#pragma warning restore CS8618 
    }
    /// <summary>
    /// 领域实体基类
    /// </summary>
    public abstract class Entity : Entity<long>, IEntity
    {
    }
}
