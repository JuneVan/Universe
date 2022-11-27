namespace Universe.Domain.Auditing
{
    public interface IDeletionAudited : IHasDeletionTime
    {
        long? DeleterUserId { get; set; }
    }
}