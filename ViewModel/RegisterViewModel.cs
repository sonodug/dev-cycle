using System;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Pages;
using wpf_game_dev_cycle.Repositories;
using wpf_game_dev_cycle.Services;
using wpf_game_dev_cycle.View;

namespace wpf_game_dev_cycle.ViewModel
{
    public class RegisterViewModel : ObservableObject
    {
        private string _login;
        private SecureString _password;
        private string _name;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private Page _pageSource;
        
        private string _errorMessage;
        private IUserRepository _userRepository;

        private readonly PageServiceFirstNest _pageServiceFirstNest;
        private readonly RegistrationService _regService;
        private readonly WindowNavigationService _navigationService;

        public Page PageSource
        {
            get { return _pageSource; }
            set
            {
                _pageSource = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get => _login;

            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public SecureString Password
        {
            get => _password;

            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        
        public string ErrorMessage
        {
            get => _errorMessage;

            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand ReturnWindowCommand { get; }
        public ICommand RegisterCommand { get; }

        public RegisterViewModel(RegistrationService regService, WindowNavigationService navigationService, PageServiceFirstNest pageServiceFirstNest)
        {
            _navigationService = navigationService;
            
            _regService = regService;

            _pageServiceFirstNest = pageServiceFirstNest;
            _pageServiceFirstNest.PageChanged += (page) => PageSource = page;

            _userRepository = new UserRepositoryControl();

            ReturnWindowCommand = new RelayCommand(ExecuteReturnWindowCommand);
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
        }

        private void ExecuteRegisterCommand(object obj)
        {
            var isValidUser = _userRepository.RegisterUser(new NetworkCredential(Login, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Login), null);
                
                //_pageServiceFirstNest.ChangePage(new EmailVerificationPage());
                _navigationService.ChangeWindow(new AdminView());
                _regService.Register();
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Login) || Login.Length < 3 || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;

            return validData;
        }

        private void ExecuteReturnWindowCommand(object obj)
        {
            _navigationService.ChangeWindow(new LoginView());
        }
    }
}