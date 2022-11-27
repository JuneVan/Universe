namespace Universe.Domain.Events
{
    public interface IEventBus
    {
        Task SendAsync<TEventData>(TEventData @event) where TEventData : IEventData;
        Task SendAsync<TEventData>(IEnumerable<TEventData> events) where TEventData : IEventData;
    }
}
