namespace Universe.Domain.Auditing
{
    [Serializable]
    public abstract class CreationAuditedAggregateRoot : AggregateRoot, ICreationAudited
    {
        public virtual DateTime CreatedOnUtc { get; set; }
        public virtual long CreatorUserId { get; set; }
        protected CreationAuditedAggregateRoot()
        {
            CreatedOnUtc = DateTime.Now;
        }
    }
}
