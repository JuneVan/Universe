namespace Universe.Domain.Events
{
    public class EntityUpdatedEvent<TEntity> : EntityChangedEvent<TEntity>
    {
        public EntityUpdatedEvent(TEntity entity)
            : base(entity)
        {

        }
    }
}
