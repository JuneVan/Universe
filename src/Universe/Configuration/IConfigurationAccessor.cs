namespace Universe.Configuration
{
    /// <summary>
    /// 应用配置访问接口
    /// </summary>
    public interface IConfigurationAccessor
    {
        /// <summary>
        /// 应用配置
        /// </summary>
        IConfigurationRoot Configuration { get; }
    }
}
