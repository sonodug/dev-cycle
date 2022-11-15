using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.View;
using wpf_game_dev_cycle.Repositories;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<Table> TableItems { get; set; }
        
        private IEnumerable _selectedTableToList;
        private Entity _selectedTableItem;

        private UserAccountModel _currentUserAccount;
        private IUserRepository _userRepository;
        private CompanyContext _database;
        private Table _selectedTable;

        public ICommand UpdateCommand { get; }
        public ICommand DeleteRowCommand { get; }

        public Entity SelectedTableItem
        {
            get => _selectedTableItem;
            set
            {
                _selectedTableItem = value;
                OnPropertyChanged();
            }
        }
        
        public Table SelectedTable
        {
            get => _selectedTable;
            set
            {
                if (value.Name == "Admin_account")
                {
                    SelectedTableToList = _database.AdministratorAccounts.Local.ToBindingList();
                }

                if (value.Name == "Publisher_account")
                {
                    SelectedTableToList = _database.PublisherAccounts.Local.ToBindingList();
                }

                _selectedTable = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable SelectedTableToList
        {
            get => _selectedTableToList;
            set
            {
                _selectedTableToList = value;
                OnPropertyChanged();
            }
        }
        
        public UserAccountModel CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _database = new CompanyContext();
            AddTables();
            InitializeSets();

            UpdateCommand = new RelayCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);
            DeleteRowCommand = new RelayCommand(ExecuteDeleteRowCommand, CanExecuteDeleteRowCommand);


            // _userRepository = new UserRepositoryControl();
            // CurrentUserAccount = new UserAccountModel();
            
            // LoadCurrentUserData();
        }
        
        private void ExecuteDeleteRowCommand(object obj)
        {
            AdministratorAccount account = SelectedTableItem as AdministratorAccount;
            if (account != null)
            {
                _database.AdministratorAccounts.Remove(account);
            }
            
            _database.SaveChanges();
        }
        
        private bool CanExecuteDeleteRowCommand(object obj)
        {
            return true;
        }
        
        private void ExecuteUpdateCommand(object obj)
        {
            _database.SaveChanges();
        }

        private bool CanExecuteUpdateCommand(object obj)
        {
            //tut cheto dolsno bit
            return true;
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

        private void AddTables()
        {
            TableItems = new ObservableCollection<Table>
            {
                new Table
                {
                    Name = "Admin_account"
                },
                new Table
                {
                    Name = "Publisher_account"
                }
            };
        }

        private void InitializeSets()
        {
            _database.AdministratorAccounts.Load();
            _database.PublisherAccounts.Load();
        }
    }
}
