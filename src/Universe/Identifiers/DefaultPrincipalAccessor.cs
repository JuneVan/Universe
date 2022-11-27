namespace Universe.Identifiers
{
    public class DefaultPrincipalAccessor : IPrincipalAccessor
    {
        public virtual ClaimsPrincipal? Principal => Thread.CurrentPrincipal as ClaimsPrincipal;
    }
}
