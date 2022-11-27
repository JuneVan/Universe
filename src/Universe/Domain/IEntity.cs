namespace Universe.Domain
{
    /// <summary>
    /// 领域实体泛型接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
    /// <summary>
    /// 领域实体接口
    /// </summary>
    public interface IEntity : IEntity<long> { }
}
