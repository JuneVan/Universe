namespace Universe.Application.Dtos
{
    public abstract class EntityDto<TKey>
    {
        public virtual TKey? Id { get; set; }
    }
    public abstract class EntityDto : EntityDto<long>
    {

    }
}
