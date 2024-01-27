using PA.Desktop.Models;
using PA.Desktop.Repositories;
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
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            IUserRepository userRepository = new UserRepository(httpClient);
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
    }
}
