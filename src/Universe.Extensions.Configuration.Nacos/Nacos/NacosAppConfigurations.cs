namespace Universe.Extensions.Configuration.Nacos
{
    public static class NacosAppConfigurations
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

        static NacosAppConfigurations()
        {
            ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot Get(string path, string? environmentName = null)
        {
            string cacheKey = path + "#" + environmentName;
            return ConfigurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName)
            );
        }
        private static IConfigurationBuilder GetConfigurationBuilder(IConfigurationBuilder builder, string path, string? environmentName = null)
        {
            builder.SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }
            return builder;
        }
        private static IConfigurationRoot BuildConfiguration(string path, string? environmentName = null)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddNacosV2Configuration(delegate (NacosV2ConfigurationSource configure)
            {
                IConfigurationRoot configurationRoot = GetConfigurationBuilder(new ConfigurationBuilder(), path, environmentName).Build();

                var nacosOptions = configurationRoot.GetSection("Nacos").Get<NacosSdkOptions>();
                if (nacosOptions == null)
                    throw new NacosException("未找到Nacos配置信息。");

                configure.ServerAddresses = nacosOptions.ServerAddresses;
                configure.DefaultTimeOut = nacosOptions.DefaultTimeOut;
                configure.Namespace = nacosOptions.Namespace;
                configure.UserName = nacosOptions.UserName;
                configure.Password = nacosOptions.Password;
                configure.ListenInterval = nacosOptions.ListenInterval;
                configure.ConfigUseRpc = false;
                configure.NamingUseRpc = false;
                List<ConfigListener> list = new();
                var listeners = configurationRoot.GetSection("Nacos:Listeners")?.Get<List<ConfigListener>>();
                if (listeners != null && listeners.Any())
                    configure.Listeners = listeners;
            });
            builder = GetConfigurationBuilder(builder, path, environmentName);
            return builder.Build();
        }
    }
}
