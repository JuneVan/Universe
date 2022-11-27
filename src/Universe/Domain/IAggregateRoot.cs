namespace Universe.Domain
{
    /// <summary>
    /// 定义泛型聚合根接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {
        IReadOnlyCollection<IEventData>? DomainEvents { get; }
    }
    /// <summary>
    /// 定义聚合根接口
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<long>
    {
    }
}
