using System.Threading;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Repositories;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private UserAccountModel _currentUserAccount;
        private IUserRepository _userRepository;

        private readonly LoginService _loginService;

        public UserAccountModel CurrentUserAccount
        {
            get { return _currentUserAccount; }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(LoginService loginService)
        {
            _userRepository = new UserRepositoryControl();
            CurrentUserAccount = new UserAccountModel();
            
            _loginService = loginService;
            _loginService.Logged += LoadCurrentUserData;
        }

        private void LoadCurrentUserData()
        {
            var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName} ;)";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
            }
        }
    }
}
