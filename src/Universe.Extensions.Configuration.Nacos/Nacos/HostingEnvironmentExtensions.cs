namespace Universe.Extensions.Configuration.Nacos
{
    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetNacosAppConfiguration(this IHostEnvironment env)
        {
            return NacosAppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }
    }
}
