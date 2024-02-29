using PA.ApplicationCore.Models;
using PA.Desktop.Converters;
using PA.Desktop.Events;
using PA.Desktop.Repositories;
using System.Diagnostics;
using System.Management;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Windows.Threading;

namespace PA.Desktop.Services
{
    public class SystemInfoService
    {
        private readonly DispatcherTimer dataCollectionTimer;
        private readonly DispatcherTimer aggregationTimer;
        private readonly PerformanceCounter cpuCounter;
        private readonly PerformanceCounter memAvailableCounter;
        private readonly PostesRepository postesRepository;
        private readonly ulong memTotal = 0;
        private float cpuUsageValue;
        private float memUsageValue;
        private int? postId;
        public string macAddress;
        public string ipAddress;
        private List<float> cpuUsages = new List<float>();
        private List<float> memUsages = new List<float>();
        public event EventHandler<CpuUsageUpdatedEventArgs> CpuUsageUpdated;
        public event EventHandler<MemUsageUpdatedEventArgs> MemUsageUpdated;

        private static SystemInfoService _instance;

        public static SystemInfoService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SystemInfoService();
                }
                return _instance;
            }
        }


        private SystemInfoService()
        {
            postesRepository = new PostesRepository(new HttpClient());

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

            dataCollectionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2),
                IsEnabled = true
            };
            dataCollectionTimer.Tick += DataCollectionTimer_Tick;
            dataCollectionTimer.Start();

            aggregationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1),
                IsEnabled = true
            };
            aggregationTimer.Tick += AggregationTimer_Tick;
            aggregationTimer.Start();

            GetNetworkInformation();
            CheckMachineExistence();
        }

        private void DataCollectionTimer_Tick(object sender, EventArgs e)
        {
            cpuUsageValue = cpuCounter.NextValue();
            memUsageValue = memTotal - memAvailableCounter.NextValue();

            cpuUsages.Add(cpuUsageValue);
            memUsages.Add(memUsageValue);

            // Raise events with the new values
            OnCpuUsageUpdated(cpuUsageValue);
            OnMemUsageUpdated(memUsageValue);
        }

        protected virtual void OnCpuUsageUpdated(float cpuUsage)
        {
            CpuUsageUpdated?.Invoke(this, new CpuUsageUpdatedEventArgs(cpuUsage));
        }

        protected virtual void OnMemUsageUpdated(float memUsage)
        {
            MemUsageUpdated?.Invoke(this, new MemUsageUpdatedEventArgs(memUsage));
        }


        private async void AggregationTimer_Tick(object sender, EventArgs e)
        {
            if (!postId.HasValue || cpuUsages.Count == 0 || memUsages.Count == 0) return;

            // Aggregate data
            var cpuMean = AggregatedDataHelper.CalculateMean(cpuUsages);
            var cpuMedian = AggregatedDataHelper.CalculateMedian(cpuUsages);
            var cpuPeak = AggregatedDataHelper.CalculatePeak(cpuUsages);

            var memMean = AggregatedDataHelper.CalculateMean(memUsages);
            var memMedian = AggregatedDataHelper.CalculateMedian(memUsages);
            var memPeak = AggregatedDataHelper.CalculatePeak(memUsages);

            // Construct aggregated data model and send to API
            var aggregatedData = new AggregatedDataModel
            {
                CpuUsageMean = cpuMean,
                CpuUsageMedian = cpuMedian,
                CpuUsagePeak = cpuPeak,
                MemoryUsageMean = memMean,
                MemoryUsageMedian = memMedian,
                MemoryUsagePeak = memPeak
            };

            // Send aggregated data to the API
            _ = await postesRepository.SendAggregatedDataToApi(postId.Value, aggregatedData);

            // Handle response and clear lists
            cpuUsages.Clear();
            memUsages.Clear();
        }

        private void GetNetworkInformation()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up)
                {
                    string unformattedMacAddress = nic.GetPhysicalAddress().ToString();

                    string formattedMacAddress = Enumerable
                        .Range(0, unformattedMacAddress.Length / 2)
                        .Select(i => unformattedMacAddress.Substring(i * 2, 2))
                        .Aggregate((partialAddress, pair) => $"{partialAddress}-{pair}");

                    // Store the formatted MAC address
                    macAddress = formattedMacAddress;
                    IPInterfaceProperties ipProperties = nic.GetIPProperties();
                    var IpAddress = ipProperties.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    ipAddress = IpAddress?.Address.ToString();
                    break;
                }
            }
        }

        private async void CheckMachineExistence()
        {
            try
            {
                var postesList = await postesRepository.GetAllPostes();
                var postes = postesList.FirstOrDefault(p => p.MacAddress == macAddress);

                if (postes != null)
                {
                    postId = postes.PostesId;
                    if (!string.Equals(postes.IpAddress, ipAddress) || postes.IpAddress.Equals(null))
                    {
                        await UpdateMachineIpAddress(postes.PostesId, ipAddress);
                    }
                }
                else
                {
                    // Machine does not exist in the database
                    // Handle accordingly (e.g., display a message or add a new entry)
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        private async Task UpdateMachineIpAddress(int postId, string newIpAddress)
        {
            await postesRepository.UpdateIpAddress(postId, newIpAddress);
        }

        // Additional methods as needed for data aggregation and transfer
    }
}
