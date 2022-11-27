namespace Universe.Identifiers
{
    public class ClaimUserIdentifier : IUserIdentifier
    {

        public ClaimUserIdentifier(IPrincipalAccessor principalAccessor)
        {
            PrincipalAccessor = principalAccessor;
        }

        protected IPrincipalAccessor PrincipalAccessor { get; }

        public long? UserId
        {
            get
            {
                Claim? userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }
                if (long.TryParse(userIdClaim.Value, out long userId))
                    return userId;
                return null;
            }
        }
    }
}
