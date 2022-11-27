namespace Universe.Threading
{
    public class NoneSignal : ISignal
    {
        public CancellationToken Token => CancellationToken.None;
        public NoneSignal()
        {
        }
    }
}
