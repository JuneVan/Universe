namespace Universe.Domain.Auditing
{
    public interface IHasDeletionTime : ISoftDelete
    {
        DateTime? DeletedOnUtc { get; set; }
    }
}