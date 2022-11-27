namespace Universe.Domain.Events
{
    public class EntityCreatedEvent<TEntity> : EntityChangedEvent<TEntity>
    {
        public EntityCreatedEvent(TEntity entity)
            : base(entity)
        {

        }
    }
}
