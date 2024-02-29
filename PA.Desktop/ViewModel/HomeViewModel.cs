using System.ComponentModel;
using System.Runtime.CompilerServices;
using PA.Desktop.Services;
using PA.Desktop.Events;
using System.Windows;

namespace PA.Desktop.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private string _macAddress;
        private string _ipAddress;
        private float _cpuUsageValue;
        private float _memUsageValue;

        private readonly SystemInfoService _systemInfoService;

        public event PropertyChangedEventHandler PropertyChanged;

        public string MacAddress
        {
            get => _macAddress;
            set => SetProperty(ref _macAddress, value);
        }

        public string IpAddress
        {
            get => _ipAddress;
            set => SetProperty(ref _ipAddress, value);
        }

        public float CpuUsageValue
        {
            get => _cpuUsageValue;
            set => SetProperty(ref _cpuUsageValue, value);
        }

        public float MemUsageValue
        {
            get => _memUsageValue;
            set => SetProperty(ref _memUsageValue, value);
        }

        public HomeViewModel(SystemInfoService systemInfoService)
        {
            _systemInfoService = systemInfoService;

            // Subscribe to events
            _systemInfoService.CpuUsageUpdated += SystemInfoService_CpuUsageUpdated;
            _systemInfoService.MemUsageUpdated += SystemInfoService_MemUsageUpdated;

            MacAddress = _systemInfoService.macAddress;
            IpAddress = _systemInfoService.ipAddress;
        }

        private void SystemInfoService_CpuUsageUpdated(object sender, CpuUsageUpdatedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CpuUsageValue = e.CpuUsage;
            });
        }


        private void SystemInfoService_MemUsageUpdated(object sender, MemUsageUpdatedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MemUsageValue = e.MemUsage;
            });
        }
    }
}
