namespace Universe.Domain.Auditing
{
    public interface IAudited : ICreationAudited, IModificationAudited
    {
    }
}