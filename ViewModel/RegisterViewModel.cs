using System.Security;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Repositories;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle.ViewModel
{
    public class RegisterViewModel : ObservableObject
    {
        private string _username;
        private SecureString _password;
        private string _email;
        private string _phoneNumber;

        private string _errorMessage;
        private IUserRepository _userRepository;

        private readonly RegistrationService _regService;
        
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

        public string Email
        {
            get => _email;

            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        
        public string PhoneNumber
        {
            get => _phoneNumber;

            set
            {
                _username = value;
                OnPropertyChanged(nameof(PhoneNumber));
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

        public RegisterViewModel(RegistrationService regService)
        {
            _regService = regService;
            _userRepository = new UserRepository();
        }
    }
}