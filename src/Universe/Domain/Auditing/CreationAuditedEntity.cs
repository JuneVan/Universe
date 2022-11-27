namespace Universe.Domain.Auditing
{
    [Serializable]
    public abstract class CreationAuditedEntity : Entity, ICreationAudited
    {
        public virtual DateTime CreatedOnUtc { get; set; }

        public virtual long CreatorUserId { get; set; }

        protected CreationAuditedEntity()
        {
            CreatedOnUtc = DateTime.Now;
        }
    }
}