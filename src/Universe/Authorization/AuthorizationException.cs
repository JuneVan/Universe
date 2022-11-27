namespace Universe.Authorization
{
    public class AuthorizationException : UniverseException
    {
        public AuthorizationException(string message) : base(message)
        {

        }
        public override LogLevel Level => base.Level;
    }
}
