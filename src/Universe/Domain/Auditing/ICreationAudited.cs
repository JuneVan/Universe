namespace Universe.Domain.Auditing
{
    public interface ICreationAudited : IHasCreationTime
    {
        long CreatorUserId { get; set; }
    }
}