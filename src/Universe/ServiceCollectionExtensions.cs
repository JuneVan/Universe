namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Universe核心类库
        /// </summary> 
        /// <returns></returns>
        public static UniverseBuilder AddUniverse(this IServiceCollection services, params Assembly[] assemblies)
        {
            UniverseBuilder builder = new(services, assemblies);

            builder.AddEventBus();
            builder.AddSignal();
            builder.AddUserIdentifier();
            builder.AddConfiguration();



            return builder;
        }
    }
}
