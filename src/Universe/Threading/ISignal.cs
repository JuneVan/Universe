namespace Universe.Threading
{
    public interface ISignal
    {
        CancellationToken Token { get; }
    }
}
