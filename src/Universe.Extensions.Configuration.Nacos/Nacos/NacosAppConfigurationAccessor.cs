namespace Universe.Extensions.Configuration.Nacos
{
    public class NacosAppConfigurationAccessor : IConfigurationAccessor
    {
        public IConfigurationRoot Configuration { get; }
        public NacosAppConfigurationAccessor(IHostEnvironment environment)
        {
            Configuration = environment.GetNacosAppConfiguration();
        }
    }
}
