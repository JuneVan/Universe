using Universe.Configuration.Nacos.TestApp;
using Universe.Extensions.Caching.Redis;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .ConfigureServices(services =>
    { 
        services.AddUniverse(AppDomain.CurrentDomain.GetAssemblies())
         .AddNacos();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
