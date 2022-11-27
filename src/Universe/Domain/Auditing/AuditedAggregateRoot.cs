namespace Universe.Domain.Auditing
{

    [Serializable]
    public abstract class AuditedAggregateRoot : CreationAuditedAggregateRoot, IAudited
    {
        public virtual DateTime? LastModifiedOnUtc { get; set; }

        public virtual long? LastModifierUserId { get; set; }
    }
}