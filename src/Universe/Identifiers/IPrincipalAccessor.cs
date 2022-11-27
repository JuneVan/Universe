namespace Universe.Identifiers
{
    public interface IPrincipalAccessor
    {
        ClaimsPrincipal? Principal { get; }
    }
}
