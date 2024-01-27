using PA.Desktop.Models;
using System.Net;
using System.Security;
using System.Windows.Input;
using PA.ApplicationCore.Domain;

namespace PA.Desktop.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private readonly IUserRepository _userRepository;

        //Properties
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        //-> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        private bool _isLoginCommandExecuting;

        //Constructor
        public LoginViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return !_isLoginCommandExecuting && validData;
        }

        private async void ExecuteLoginCommand(object obj)
        {
            _isLoginCommandExecuting = true;
            CommandManager.InvalidateRequerySuggested();
            string? plainPassword = null;
            try
            {
                plainPassword = new NetworkCredential(string.Empty, _password).Password;

                var loginModel = new LoginModel
                {
                    Username = _username,
                    Password = plainPassword
                };

                var isValidUser = await _userRepository.AuthenticateUser(loginModel);
                if (isValidUser)
                {
                    IsViewVisible = false;
                }
                else
                {
                    ErrorMessage = "* Invalid username or password";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred during login.";
            }
            finally
            {
                plainPassword = null;
            }

            _isLoginCommandExecuting = false;
            CommandManager.InvalidateRequerySuggested();
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
