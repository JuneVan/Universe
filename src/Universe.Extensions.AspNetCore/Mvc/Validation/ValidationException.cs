namespace Universe.Extensions.AspNetCore.Mvc.Validation
{
    /// <summary>
    /// 定义验证异常
    /// </summary> 
    public class ValidationException : UniverseException
    {
        public override LogLevel Level => LogLevel.Warning;

        public ValidationException(string message)
           : base(message)
        {
        }
    }
}
