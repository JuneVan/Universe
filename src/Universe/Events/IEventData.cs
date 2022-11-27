namespace Universe.Domain.Events
{
    public interface IEventData : INotification
    {
        DateTime CreatedOnUtc { get; set; }
        object EventSource { get; set; }
    }
}
