using System;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Pages;
using wpf_game_dev_cycle.Repositories;
using wpf_game_dev_cycle.Services;
using wpf_game_dev_cycle.View;

namespace wpf_game_dev_cycle.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;

        private IUserRepository _userRepository;

        private readonly LoginService _loginService;
        private readonly WindowNavigationService _navigationService;
        private readonly PageService _pageService;
        
        private readonly ILogger<RelayCommand> _logger;


        public string Username
        {
            get => _username;

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get => _password;

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand CreateAccountCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        
        public LoginViewModel(LoginService loginService, WindowNavigationService navigationService,
            PageService pageService)
        {
            _pageService = pageService;
            
            _loginService = loginService;

            _navigationService = navigationService;

            _userRepository = new UserRepositoryControl();

            CommandLoggerFactory loggerFactory = new CommandLoggerFactory(); 
            _logger = loggerFactory.CreateLogger<RelayCommand>();
            
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            CreateAccountCommand = new RelayCommand(ExecuteCreateAccountCommand);
            RecoverPasswordCommand = new RelayCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            _logger.LogInformation("Loging starting");
            
            var isValidUser = _userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                
                _navigationService.ChangeWindow(new MainWindow());
                _loginService.Login();
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
            
            _logger.LogInformation("Loging succesfully executed");
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private void ExecuteCreateAccountCommand(object obj)
        {
            _logger.LogInformation("Create account window");
            _navigationService.ChangeWindow(new RegisterView());
        }
    }
}
