namespace Universe.Configuration.Nacos.TestApp
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfigurationAccessor _configurationAccessor;
        public Worker(ILogger<Worker> logger,
            IConfigurationAccessor configurationAccessor)
        {
            _logger = logger;
            _configurationAccessor = configurationAccessor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation($"test_value: {_configurationAccessor.Configuration["test_value"]}");
            await Task.CompletedTask;

        }
    }
}