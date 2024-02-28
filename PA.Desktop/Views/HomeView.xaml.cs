using PA.Desktop.ViewModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Management;
using System.Net.Http;

namespace PA.Desktop.Views
{
    /// <summary>
    /// Logique d'interaction pour HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        private readonly PerformanceCounter cpuCounter;
        private readonly PerformanceCounter memAvailableCounter;
        private readonly ulong memTotal = 0;

        private readonly DispatcherTimer timer;

        private const int KB = 1024;
        private const int MB = KB * 1024;
        private const int GB = MB * 1024;
        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeViewModel(new HttpClient());
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            memAvailableCounter = new PerformanceCounter("Memory", "Available Bytes");

            using (var memMC = new ManagementClass("Win32_PhysicalMemory"))
            {
                using var memMOC = memMC.GetInstances();
                foreach (var memMO in memMOC)
                {
                    memTotal += (ulong)memMO.GetPropertyValue("Capacity");
                }
            }
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 2),
                IsEnabled = true
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            float cpuUsageValue = cpuCounter.NextValue();
            var memUsed = memTotal - memAvailableCounter.NextValue();

            var viewModel = DataContext as HomeViewModel;
            if (viewModel != null)
            {
                viewModel.CpuUsageValue = cpuUsageValue;
                viewModel.MemUsageValue = memUsed / memTotal;

                // Add the collected values to the lists for aggregation
                viewModel.AddCpuUsage(cpuUsageValue);
                viewModel.AddMemUsage(memUsed);
            }

            CpuUsage.Content = $"{cpuUsageValue:F2}%";
            int progressBarValue = (int)Math.Round(cpuUsageValue);
            progressBarValue = Math.Min(progressBarValue, 100); // Ensure it doesn't exceed 100
            CpuProgressBar.Value = progressBarValue;

            MemUsage.Content = $"{memUsed / memTotal:P2} ({memUsed / GB:F2}GB/{memTotal / GB:F2}GB)";
        }

    }
}
