namespace Universe.Extensions.Caching.Redis
{
    public static class UniverseBuilderExtensions
    {
        /// <summary>
        /// 添加Nacos功能
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static UniverseBuilder AddNacos(this UniverseBuilder builder)
        {
            builder.Services.Replace(new ServiceDescriptor(typeof(IConfigurationAccessor), typeof(NacosAppConfigurationAccessor), ServiceLifetime.Scoped));
            return builder;
        }
    }
}
