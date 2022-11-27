namespace Universe
{
    public class UniverseException : Exception, IHasLogLevel
    {
        public UniverseException(string message) : base(message)
        {

        }
        public UniverseException(string message, Exception? innerException) : base(message, innerException)
        {

        }

        public virtual LogLevel Level => LogLevel.Error;
    }
}
