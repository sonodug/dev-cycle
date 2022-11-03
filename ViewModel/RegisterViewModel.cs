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
        private string _username;
        private SecureString _password;
        private string _name;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private Page _pageSource;
        
        private string _errorMessage;
        private IUserRepository _userRepository;

        private readonly PageService _pageService;
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

        public string Username
        {
            get => _username;

            set
            {
                _username = value;
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

        public string Name
        {
            get => _name;

            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        
        public string LastName
        {
            get => _lastName;

            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        
        public string Email
        {
            get => _email;

            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        
        public string PhoneNumber
        {
            get => _phoneNumber;

            set
            {
                _phoneNumber = value;
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
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        
        public RegisterViewModel(RegistrationService regService, WindowNavigationService navigationService, PageService pageService)
        {
            _navigationService = navigationService;
            
            _regService = regService;

            _pageService = pageService;
            _pageService.PageChanged += (page) => PageSource = page;

            _userRepository = new UserRepositoryControl();

            ReturnWindowCommand = new RelayCommand(ExecuteReturnWindowCommand);
            RegisterCommand = new RelayCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            RecoverPasswordCommand = new RelayCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        private void ExecuteRegisterCommand(object obj)
        {
            var isValidUser = _userRepository.RegisterUser(new NetworkCredential(Username, Password), Name, LastName, Email, PhoneNumber);
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                
                //tut changepage na email ver-n
                _navigationService.ChangeWindow(new MainWindow());
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
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;

            return validData;
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private void ExecuteReturnWindowCommand(object obj)
        {
            //navigationService.ChangeWindow(new LoginView());
            _pageService.ChangePage(new EmailVerificationPage());
        }
    }
}