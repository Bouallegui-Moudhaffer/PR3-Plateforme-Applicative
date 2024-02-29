using Hardcodet.Wpf.TaskbarNotification;
using PA.Desktop.Models;
using PA.Desktop.Repositories;
using PA.Desktop.Services;
using PA.Desktop.Views;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _ = SystemInfoService.Instance;

            httpClient = new HttpClient();

            // Initialize UserRepository with the HttpClient instance
            userRepository = new UserRepository(httpClient);

            // Initialize and display NotifyIcon from resources
            notifyIcon = (TaskbarIcon)FindResource("MyNotifyIcon");
        }

        protected override void OnExit(ExitEventArgs e)
        {
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
