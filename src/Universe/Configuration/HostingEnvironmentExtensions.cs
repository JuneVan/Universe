namespace Universe.Configuration
{
    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IHostEnvironment env)
        {
            return Configurations.Get(env.ContentRootPath, env.EnvironmentName);
        }
    }
}
