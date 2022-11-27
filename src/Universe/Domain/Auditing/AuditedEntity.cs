namespace Universe.Domain.Auditing
{
    [Serializable]
    public abstract class AuditedEntity : CreationAuditedEntity, IAudited, IEntity
    {
        public virtual DateTime? LastModifiedOnUtc { get; set; }
        public virtual long? LastModifierUserId { get; set; }
    }
}