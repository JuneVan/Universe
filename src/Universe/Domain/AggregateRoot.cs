namespace Universe.Domain
{
    /// <summary>
    /// 领域聚合根实体泛型基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    {
        private List<IEventData> _domainEvents;
        public IReadOnlyCollection<IEventData> DomainEvents => _domainEvents;
        public AggregateRoot()
        {
            _domainEvents = new List<IEventData>();
        }
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        protected void AddDomainEvent(IEventData domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
    /// <summary>
    /// 领域聚合根实体基类
    /// </summary>
    public abstract class AggregateRoot : AggregateRoot<long>, IAggregateRoot { }
}
