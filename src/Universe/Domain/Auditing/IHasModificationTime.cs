namespace Universe.Domain.Auditing
{
    public interface IHasModificationTime
    {
        DateTime? LastModifiedOnUtc { get; set; }
    }
}