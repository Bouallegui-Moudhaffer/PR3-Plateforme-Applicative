using PA.ApplicationCore.Domain;
using PA.Desktop.Models;
using System.Windows;
using System.Windows.Input;

namespace PA.Desktop.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly IUserRepository userRepository;
        public bool IsLoginSuccessful { get; private set; }
        public LoginView(IUserRepository userRepository)
        {
            InitializeComponent();
            this.userRepository = userRepository;
        }
        private void HandleSuccessfulLogin()
        {
            IsLoginSuccessful = true;
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPass.Password;

            var loginModel = new LoginModel { Username = username, Password = password };

            bool isAuthenticated = await AuthenticateUser(loginModel);

            if (isAuthenticated)
            {
                IsLoginSuccessful = true;
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                // Login successful, navigate to MainView
                MainView mainView = new MainView();
                mainView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task<bool> AuthenticateUser(LoginModel loginModel)
        {
            try
            {
                return await userRepository.AuthenticateUser(loginModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during authentication: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
