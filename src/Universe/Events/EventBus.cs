using Newtonsoft.Json;

namespace Universe.Domain.Events
{
    public class EventBus : IEventBus
    {
        private readonly IPublisher _publisher;
        private readonly ISignal _signal;
        private readonly ILogger _logger;
        public EventBus(IPublisher publisher,
             ISignal signal,
             ILogger<EventBus> logger)
        {
            _publisher = publisher;
            _signal = signal;
            _logger = logger;
        }
        public async Task SendAsync<TEventData>(TEventData @event) where TEventData : IEventData
        {
            await _publisher.Publish(@event, _signal.Token);
        }

        public async Task SendAsync<TEventData>(IEnumerable<TEventData> events) where TEventData : IEventData
        {
            if (!events.Any())
                return;
            foreach (var @event in events)
            {
                _logger.LogDebug($"发布领域事件`{@event.GetType()}`-`{JsonConvert.SerializeObject(@event)}`");
                await _publisher.Publish(@event, _signal.Token);
            }
        }
    }
}
