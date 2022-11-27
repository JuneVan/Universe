namespace Universe.Domain.Auditing
{
    public interface IHasCreationTime
    {
        DateTime CreatedOnUtc { get; set; }
    }
}