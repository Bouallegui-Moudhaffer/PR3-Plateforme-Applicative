﻿using FontAwesome.Sharp;
using PA.Desktop.Models;
using PA.Desktop.Repositories;
using PA.Desktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PA.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;
        private readonly HttpClient httpClient;

        //Properties
        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }

            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Commands
        public ICommand ShowHomeViewCommand { get; }

        public MainViewModel()
        {
            httpClient = new HttpClient();
            userRepository = new UserRepository(httpClient);
            CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);

            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }
        private void LoadCurrentUserData()
        {
            if (Thread.CurrentPrincipal?.Identity != null && !string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name))
            {
                var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

                if (user == null)
                {
                    CurrentUserAccount.Username = user.Result.Username;
                    CurrentUserAccount.DisplayName = $"{user.Result.Nom}";
                    CurrentUserAccount.ProfilePicture = null;
                }
                else
                {
                    CurrentUserAccount.DisplayName = "User not logged in";
                }
            }
            else
            {
                // Handle the case where the principal or identity is not set
            }
        }
        private void ExecuteShowHomeViewCommand(object obj)
        {
            // Assuming SystemInfoService.Instance is the way to access your singleton instance
            CurrentChildView = new HomeViewModel(SystemInfoService.Instance);
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }
    }
}
