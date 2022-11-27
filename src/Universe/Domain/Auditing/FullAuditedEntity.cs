namespace Universe.Domain.Auditing
{

    [Serializable]
    public abstract class FullAuditedEntity : AuditedEntity, IFullAudited
    {
        public virtual bool IsDeleted { get; set; }

        public virtual long? DeleterUserId { get; set; }

        public virtual DateTime? DeletedOnUtc { get; set; }
    }
}