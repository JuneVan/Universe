namespace Universe.Extensions.AspNetCore.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute : Attribute
    {
        public bool WrapOnSuccess { get; set; }
        public bool WrapOnError { get; set; }
        public bool LogError { get; set; }
        public WrapResultAttribute(bool wrapOnSuccess = true, bool wrapOnError = true)
        {
            WrapOnSuccess = wrapOnSuccess;
            WrapOnError = wrapOnError;
            LogError = true;
        } 
        public static WrapResultAttribute DefaultWrapResult => new WrapResultAttribute();

    }
}
