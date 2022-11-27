namespace Universe.Domain.Events
{
    public class EventData : IEventData
    {
        public DateTime CreatedOnUtc { get; set; }
        public object? EventSource { get; set; }
    }
}
