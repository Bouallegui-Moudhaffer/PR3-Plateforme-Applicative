using PA.Desktop.Repositories;
using System.ComponentModel;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using PA.Desktop.Converters;
using System.Windows.Threading;
using PA.ApplicationCore.Models;
using System.Diagnostics;

namespace PA.Desktop.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _macAddress;
        private string _ipAddress;
        private float _cpuUsageValue;
        private float _memUsageValue;
        private readonly PostesRepository _postesRepository;
        private readonly DispatcherTimer aggregationTimer;
        private List<float> cpuUsages = new List<float>();
        private List<float> memUsages = new List<float>();
        private int? postId;

        public string MacAddress
        {
            get => _macAddress;
            set
            {
                _macAddress = value;
                OnPropertyChanged();
            }
        }

        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;
                OnPropertyChanged();
            }
        }
        public float CpuUsageValue
        {
            get => _cpuUsageValue;
            set
            {
                if (_cpuUsageValue != value)
                {
                    _cpuUsageValue = value;
                    cpuUsages.Add(value);
                    OnPropertyChanged();
                }
            }
        }
        public float MemUsageValue
        {
            get => _memUsageValue;
            set
            {
                if (_memUsageValue != value)
                {
                    _memUsageValue = value;
                    memUsages.Add(value);
                    OnPropertyChanged();
                }
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public HomeViewModel(HttpClient httpClient)
        {
            _postesRepository = new PostesRepository(httpClient);
            GetNetworkInformation();
            CheckMachineExistence();
            
            aggregationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(1),
            };
            aggregationTimer.Tick += AggregationTimer_Tick;
            aggregationTimer.Start();
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
                    MacAddress = formattedMacAddress;
                    IPInterfaceProperties ipProperties = nic.GetIPProperties();
                    var ipAddress = ipProperties.UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                    IpAddress = ipAddress?.Address.ToString();
                    break;
                }
            }
        }
        private async void CheckMachineExistence()
        {
            try
            {
                var postesList = await _postesRepository.GetAllPostes();
                var postes = postesList.FirstOrDefault(p => p.MacAddress == MacAddress);

                if (postes != null)
                {
                    postId = postes.PostesId;
                    if (!string.Equals(postes.IpAddress, IpAddress) || postes.IpAddress.Equals(null))
                    {
                        await UpdateMachineIpAddress(postes.PostesId, IpAddress);
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
            await _postesRepository.UpdateIpAddress(postId, newIpAddress);
        }
        private async void AggregationTimer_Tick(object sender, EventArgs e)
        {
            if (!postId.HasValue)
            {
                return;
            }

            // Ensure both lists contain data before proceeding with aggregation
            if (cpuUsages.Count > 0 && memUsages.Count > 0)
            {
                // Calculate aggregated data
                var cpuMean = AggregatedDataHelper.CalculateMean(cpuUsages);
                var cpuMedian = AggregatedDataHelper.CalculateMedian(cpuUsages);
                var cpuPeak = AggregatedDataHelper.CalculatePeak(cpuUsages);

                var memMean = AggregatedDataHelper.CalculateMean(memUsages);
                var memMedian = AggregatedDataHelper.CalculateMedian(memUsages);
                var memPeak = AggregatedDataHelper.CalculatePeak(memUsages);

                // Construct aggregated data model
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
                var response = await _postesRepository.SendAggregatedDataToApi(postId.Value, aggregatedData);
                if (!response.IsSuccessStatusCode)
                {
                    // Handle the error, log it, or display a message to the user
                }

                // Clear the lists for the next aggregation period
                cpuUsages.Clear();
                memUsages.Clear();
            }
            else
            {
                // Log or handle the case where there is not enough data to aggregate
                Debug.WriteLine("Not enough data to aggregate.");
            }
        }

        public void AddCpuUsage(float cpuUsage)
        {
            cpuUsages.Add(cpuUsage);
        }
        public void AddMemUsage(float memUsage)
        {
            memUsages.Add(memUsage);
        }
    }
}
