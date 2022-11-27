namespace Universe.Domain.Auditing
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
