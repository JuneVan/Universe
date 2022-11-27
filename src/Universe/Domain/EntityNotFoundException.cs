namespace Universe.Domain
{
    public class EntityNotFoundException : UniverseException
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }
        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {
            EntityType = entityType;
            Id = id;
        }
        public EntityNotFoundException(Type entityType, object id, Exception? innerException)
            : base($"无法找到类型` {entityType.Name}`, id`{id}`的记录", innerException)
        {

        }

        public override LogLevel Level => LogLevel.Warning;

        public Type? EntityType { get; set; }
        public object? Id { get; set; }
    }
}
