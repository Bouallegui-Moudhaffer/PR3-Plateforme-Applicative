using Hardcodet.Wpf.TaskbarNotification;
using PA.Desktop.Models;
using PA.Desktop.Repositories;
using PA.Desktop.Services;
using PA.Desktop.Views;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;

namespace PA.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;
        private IUserRepository userRepository;
        private HttpClient httpClient;
        private SignalRService signalRService;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var systemInfoService = SystemInfoService.Instance;

            httpClient = new HttpClient();
            userRepository = new UserRepository(httpClient);
            notifyIcon = (TaskbarIcon)FindResource("MyNotifyIcon");
            
            bool postIdReady = await systemInfoService.PostIdReadyTask;

            if (postIdReady && systemInfoService.postId.HasValue)
            {
                signalRService = new SignalRService(systemInfoService.postId.Value.ToString());
                await signalRService.StartAsync();
            }
            else
            {
                // Handle the case where postId could not be obtained
                Debug.WriteLine("PostId is not available or could not be obtained.");
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (signalRService != null)
            {
                await signalRService.StopAsync();
            }
            notifyIcon.Dispose();
            base.OnExit(e);
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView(userRepository);
            loginView.Closed += (s, ev) =>
            {
                if (loginView.IsLoginSuccessful)
                {
                    Console.WriteLine("Successful Login!");
                }
            };
            loginView.Show();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Current.Shutdown();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }
    }
}
