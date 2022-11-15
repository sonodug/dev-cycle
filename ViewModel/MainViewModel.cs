using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.View;
using wpf_game_dev_cycle.Repositories;
using wpf_game_dev_cycle.Services;

#pragma warning disable CS8600

namespace wpf_game_dev_cycle.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly CompanyContext _database;

        private Table? _selectedTable;
        private IEnumerable _selectedTableToList;
        private Entity _selectedTableItem;
        
        private string _selectedComparisons;
        private string _expressionLabel;

        private UserAccountModel _currentUserAccount;
        private IUserRepository _userRepository;
        
        public ObservableCollection<Table> TableItems { get; set; }

        public ICommand UpdateCommand { get; }
        public ICommand DeleteRowCommand { get; }
        public ICommand SelectCommand { get; }

        public string SelectedComparisons
        {
            get => _selectedComparisons;
            set
            {
                _selectedComparisons = value;
                OnPropertyChanged();
            }
        }
        public string ExpressionLabel
        {
            get => _expressionLabel;
            set
            {
                _expressionLabel = value;
                OnPropertyChanged();
            }
        }
        
        public Entity SelectedTableItem
        {
            get => _selectedTableItem;
            set
            {
                _selectedTableItem = value;
                OnPropertyChanged();
            }
        }
        
        public Table? SelectedTable
        {
            get => _selectedTable;
            set
            {
                if (value.Name == "Admin_account")
                {
                    SelectedTableToList = _database.AdministratorAccounts.Local.ToBindingList();
                    ExpressionLabel = "Selecting by Account_id";
                }
                else if (value.Name == "Employees")
                {
                    SelectedTableToList = _database.Employees.Local.ToBindingList();
                    ExpressionLabel = "Selecting by Employee_code";
                }
                else if (value.Name == "Publisher")
                {
                    SelectedTableToList = _database.Publishers.Local.ToBindingList();
                    ExpressionLabel = "Selecting by Employee_code";
                }
                else if (value.Name == "Publisher_account")
                {
                    SelectedTableToList = _database.PublisherAccounts.Local.ToBindingList();
                    ExpressionLabel = "Selecting by Account_id";
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
            SelectCommand = new RelayCommand(ExecuteSelectCommand, CanExecuteSelectCommand);
            
            // _userRepository = new UserRepositoryControl();
            // CurrentUserAccount = new UserAccountModel();

            // LoadCurrentUserData();
        }
        
        private void ExecuteDeleteRowCommand(object obj)
        {
            //visitor
            try
            {
                switch (SelectedTable.Name)
                {
                    case "Admin_account":
                    {
                        AdministratorAccount account = SelectedTableItem as AdministratorAccount;
                        if (account != null)
                        {
                            _database.AdministratorAccounts.Remove(account);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Employees":
                    {
                        Employee employee = SelectedTableItem as Employee;
                        if (employee != null)
                        {
                            _database.Employees.Remove(employee);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Publisher":
                    {
                        Publisher publisher = SelectedTableItem as Publisher;
                        if (publisher != null)
                        {
                            _database.Publishers.Remove(publisher);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Publisher_account":
                    {
                        PublisherAccount account = SelectedTableItem as PublisherAccount;
                        if (account != null)
                        {
                            _database.PublisherAccounts.Remove(account);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                
            }
        }
        
        private bool CanExecuteDeleteRowCommand(object obj)
        {
            return true;
        }
        
        private void ExecuteUpdateCommand(object obj)
        {
            try
            {
                _database.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                
            }
        }

        private bool CanExecuteUpdateCommand(object obj)
        {
            //tut cheto dolsno bit
            return true;
        }
        
        private void ExecuteSelectCommand(object obj)
        {
            //ДУБЛЯЖ БЛЯТЬ КОДА
            using (var context = new CompanyContext())
            {
                if (SelectedTable.Name == "Admin_account")
                {
                    if (int.TryParse(SelectedComparisons, out var id))
                    {
                        var accounts = _database.AdministratorAccounts.Where(p=> p.Account_id == id);
                        SelectedTableToList = accounts.ToList();
                    }
                    else
                        return;
                }
                else if (SelectedTable.Name == "Employees")
                {
                    var accounts = _database.Employees.Where(p=> p.Employee_code == SelectedComparisons);
                    SelectedTableToList = accounts.ToList();
                }
                else if (SelectedTable.Name == "Publisher")
                {
                    var accounts = _database.Publishers.Where(p=> p.Employee_code == SelectedComparisons);
                    SelectedTableToList = accounts.ToList();
                }
                else if (SelectedTable.Name == "Publisher_account")
                {
                    if (int.TryParse(SelectedComparisons, out var id))
                    {
                        var accounts = _database.PublisherAccounts.Where(p=> p.Account_id == id);
                        SelectedTableToList = accounts.ToList();
                    }
                    else
                        return;
                }
            }
        }

        private bool CanExecuteSelectCommand(object obj)
        {
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
                    Name = "Employees"
                },
                new Table
                {
                    Name = "Publisher"
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
            _database.Employees.Load();
            _database.Publishers.Load();
            _database.PublisherAccounts.Load();
        }
    }
}
