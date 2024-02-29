namespace PA.Desktop.Events
{
    public class MemUsageUpdatedEventArgs : EventArgs
    {
        public float MemUsage { get; }

        public MemUsageUpdatedEventArgs(float memUsage)
        {
            MemUsage = memUsage;
        }
    }
}
