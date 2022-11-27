namespace Universe
{
    public class UniverseBuilder
    {
        public IServiceCollection Services { get; }
        public Assembly[] Assemblies { get; }
        public UniverseBuilder(IServiceCollection services, params Assembly[] assemblies)
        {
            Services = services;
            Assemblies = assemblies;
        }

        internal UniverseBuilder AddEventBus()
        {
            if (Assemblies.Any())
            {
                Services.AddMediatR(Assemblies);
                Services.AddScoped<IEntityChangeEventHelper, EntityChangeEventHelper>();
                Services.AddScoped<IPublisher, AsyncContinueMediator>();
                Services.AddScoped<IEventBus, EventBus>();
            }
            return this;
        }
        internal UniverseBuilder AddSignal()
        {
            Services.AddScoped<ISignal, NoneSignal>();
            return this;
        }

        internal UniverseBuilder AddUserIdentifier()
        {
            Services.AddScoped<IUserIdentifier, ClaimUserIdentifier>();
            Services.AddScoped<IPrincipalAccessor, DefaultPrincipalAccessor>();
            return this;
        }


        internal UniverseBuilder AddConfiguration()
        {
            Services.AddSingleton<IConfigurationAccessor, ConfigurationAccessor>();
            return this;
        }
    }
}