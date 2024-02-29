namespace PA.Desktop.Events
{
    public class CpuUsageUpdatedEventArgs : EventArgs
    {
        public float CpuUsage { get; }

        public CpuUsageUpdatedEventArgs(float cpuUsage)
        {
            CpuUsage = cpuUsage;
        }
    }
}
