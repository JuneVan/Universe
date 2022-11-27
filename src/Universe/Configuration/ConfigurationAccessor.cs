namespace Universe.Configuration
{
    public class ConfigurationAccessor : IConfigurationAccessor
    {
        public IConfigurationRoot Configuration { get; }

        public ConfigurationAccessor(IHostEnvironment env)
        {
            Configuration = env.GetAppConfiguration();
        }
    }
}
