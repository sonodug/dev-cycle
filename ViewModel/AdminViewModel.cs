using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle.ViewModel
{
    public class AdminViewModel : ObservableObject
    {
        private CompanyContext _database;

        private Table? _selectedTable;
        private IEnumerable _selectedTableToList;
        private Entity _selectedTableItem;
        
        private string _sqlExecutionText;
        private string _selectedComparisons;
        private string _expressionLabel;

        private UserAccountModel _currentUserAccount;
        private IUserRepository _userRepository;

        private Page _mainPageSource;

        private PageServiceFirstNest _pageServiceFirstNest;

        private int _frameColumn;
        private int _frameColumnSpan;
        private Visibility _menuVisibility;

        public ObservableCollection<Table> TableItems { get; set; }

        public ICommand UpdateCommand { get; }
        public ICommand DeleteRowCommand { get; }
        public ICommand SelectCommand { get; }
        public ICommand SqlCommand { get; }
        
        public ICommand ReloadAppCommand { get; }
        public ICommand HideMenuCommand { get; }

        public int FrameColumn
        {
            get => _frameColumn;
            set
            {
                _frameColumn = value;
                OnPropertyChanged();
            }
        }
        
        public int FrameColumnSpan
        {
            get => _frameColumnSpan;
            set
            {
                _frameColumnSpan = value;
                OnPropertyChanged();
            }
        }
        public Visibility MenuVisibility
        {
            get => _menuVisibility;
            set
            {
                _menuVisibility = value;
                OnPropertyChanged();
            }
        }
        
        public Page MainPageSource
        {
            get => _mainPageSource;
            set
            {
                _mainPageSource = value;
                OnPropertyChanged();
            }
        }
        
        public string SqlExecutionText
        {
            get => _sqlExecutionText;
            set
            {
                _sqlExecutionText = value;
                OnPropertyChanged();
            }
        }
        
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
                UpdateCurrentInterfaceTable(value);

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

        public AdminViewModel(PageServiceFirstNest pageServiceFirstNest)
        {
            FrameColumn = 1;
            FrameColumnSpan = 3;
            MenuVisibility = Visibility.Visible;

            _database = new CompanyContext();
            AddTablesList();
            InitializeSets();

            UpdateCommand = new RelayCommand(ExecuteUpdateCommand);
            DeleteRowCommand = new RelayCommand(ExecuteDeleteRowCommand);
            SelectCommand = new RelayCommand(ExecuteSelectCommand);
            SqlCommand = new RelayCommand(ExecuteSqlCommand);;
            ReloadAppCommand = new RelayCommand(ExecuteReloadAppCommand);;
            HideMenuCommand = new RelayCommand(ExecuteHideMenuCommand);;

            _pageServiceFirstNest = pageServiceFirstNest;
            _pageServiceFirstNest.PageChanged += (page) => MainPageSource = page;
            
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
                    case "Admin_accounts":
                    {
                        AdministratorAccount account = SelectedTableItem as AdministratorAccount;
                        if (account != null)
                        {
                            _database.AdministratorAccounts.Remove(account);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Publishers":
                    {
                        Publisher publisher = SelectedTableItem as Publisher;
                        if (publisher != null)
                        {
                            _database.Publishers.Remove(publisher);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Publisher_accounts":
                    {
                        PublisherAccount publisherAccount = SelectedTableItem as PublisherAccount;
                        if (publisherAccount != null)
                        {
                            _database.PublisherAccounts.Remove(publisherAccount);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Verifications":
                    {
                        Verification verification = SelectedTableItem as Verification;
                        if (verification != null)
                        {
                            _database.Verifications.Remove(verification);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Developers":
                    {
                        Developer developer = SelectedTableItem as Developer;
                        if (developer != null)
                        {
                            _database.Developers.Remove(developer);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Developer_accounts":
                    {
                        DeveloperAccount account = SelectedTableItem as DeveloperAccount;
                        if (account != null)
                        {
                            _database.DeveloperAccounts.Remove(account);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Customers":
                    {
                        Customer customer = SelectedTableItem as Customer;
                        if (customer != null)
                        {
                            _database.Customers.Remove(customer);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Contracts":
                    {
                        Contract contract = SelectedTableItem as Contract;
                        if (contract != null)
                        {
                            _database.Contracts.Remove(contract);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Projects":
                    {
                        Project project = SelectedTableItem as Project;
                        if (project != null)
                        {
                            _database.Projects.Remove(project);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Repositories":
                    {
                        Repository repository = SelectedTableItem as Repository;
                        if (repository != null)
                        {
                            _database.Repositories.Remove(repository);
                        }
            
                        _database.SaveChanges();
                        break;
                    }
                    case "Development_teams":
                    {
                        DevelopmentTeam developmentTeam = SelectedTableItem as DevelopmentTeam;
                        if (developmentTeam != null)
                        {
                            _database.DevelopmentTeams.Remove(developmentTeam);
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

        private void ExecuteReloadAppCommand(object obj)
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        
        private void ExecuteHideMenuCommand(object obj)
        {
            if (MenuVisibility == Visibility.Visible)
            {
                FrameColumn = 0;
                FrameColumnSpan = 4;
                MenuVisibility = Visibility.Hidden;
            }
            else
            {
                FrameColumn = 1;
                FrameColumnSpan = 3;
                MenuVisibility = Visibility.Visible;
            }
        }

        private void ExecuteSelectCommand(object obj)
        {
            using (var context = new CompanyContext())
            {
                if (SelectedTable.Name == "Admin_accounts")
                {
                    var accounts = _database.AdministratorAccounts.Where(p=> p.Login == SelectedComparisons);
                    SelectedTableToList = accounts.ToList();
                }
                else if (SelectedTable.Name == "Publishers")
                {
                    var publishers = _database.Publishers.Where(p=> p.Full_name == SelectedComparisons);
                    SelectedTableToList = publishers.ToList();
                }
                else if (SelectedTable.Name == "Publisher_accounts")
                {
                    var accounts = _database.PublisherAccounts.Where(p=> p.Login == SelectedComparisons);
                    SelectedTableToList = accounts.ToList();
                }
                else if (SelectedTable.Name == "Verifications")
                {
                    var verifications = _database.Verifications.Where(p=> p.Employee_full_name == SelectedComparisons);
                    SelectedTableToList = verifications.ToList();
                }
                else if (SelectedTable.Name == "Developers")
                {
                    var developers = _database.Developers.Where(p=> p.Full_name == SelectedComparisons);
                    SelectedTableToList = developers.ToList();
                }
                else if (SelectedTable.Name == "Developer_accounts")
                {
                    var developerAccounts = _database.DeveloperAccounts.Where(p=> p.Login == SelectedComparisons);
                    SelectedTableToList = developerAccounts.ToList();
                }
                else if (SelectedTable.Name == "Customers")
                {
                    if (int.TryParse(SelectedComparisons, out var id))
                    {
                        var customers = _database.Customers.Where(p=> p.Customer_id == id);
                        SelectedTableToList = customers.ToList();
                    }
                    else
                        return;
                }
                else if (SelectedTable.Name == "Contracts")
                {
                    if (int.TryParse(SelectedComparisons, out var id))
                    {
                        var contracts = _database.Contracts.Where(p=> p.Contract_id == id);
                        SelectedTableToList = contracts.ToList();
                    }
                    else
                        return;
                }
                else if (SelectedTable.Name == "Projects")
                {
                    if (int.TryParse(SelectedComparisons, out var id))
                    {
                        var projects = _database.Projects.Where(p=> p.Project_id == id);
                        SelectedTableToList = projects.ToList();
                    }
                    else
                        return;
                }
                else if (SelectedTable.Name == "Repositories")
                {
                    if (int.TryParse(SelectedComparisons, out var id))
                    {
                        var repositories = _database.Repositories.Where(p=> p.Repository_id == id);
                        SelectedTableToList = repositories.ToList();
                    }
                    else
                        return;
                }
                else if (SelectedTable.Name == "Development_teams")
                {
                    if (int.TryParse(SelectedComparisons, out var id))
                    {
                        var developmentTeams = _database.DevelopmentTeams.Where(p=> p.Team_member_id == id);
                        SelectedTableToList = developmentTeams.ToList();
                    }
                    else
                        return;
                }
            }
        }

        private void ExecuteSqlCommand(object obj)
        {
            try
            {
                _database.Database.ExecuteSqlCommand(SqlExecutionText);
                _database = new CompanyContext();
                InitializeSets();
                UpdateCurrentInterfaceTable(SelectedTable);
            }
            catch (Exception e)
            {
                // mb??
            }
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

        private void AddTablesList()
        {
            TableItems = new ObservableCollection<Table>
            {
                new Table
                {
                    Name = "Admin_accounts"
                },
                new Table
                {
                    Name = "Publishers"
                },
                new Table
                {
                    Name = "Publisher_accounts"
                },
                new Table
                {
                    Name = "Verifications"
                },
                new Table
                {
                    Name = "Developers"
                },
                new Table
                {
                    Name = "Developer_accounts"
                },
                new Table
                {
                    Name = "Customers"
                },
                new Table
                {
                    Name = "Contracts"
                },
                new Table
                {
                    Name = "Projects"
                },
                new Table
                {
                    Name = "Repositories"
                },
                new Table
                {
                    Name = "Development_teams"
                }
            };
        }

        private void InitializeSets()
        {
            _database.AdministratorAccounts.Load();
            _database.Publishers.Load();
            _database.PublisherAccounts.Load();
            _database.Verifications.Load();
            _database.Developers.Load();
            _database.DeveloperAccounts.Load();
            _database.Customers.Load();
            _database.Contracts.Load();
            _database.Projects.Load();
            _database.Repositories.Load();
            _database.DevelopmentTeams.Load();
        }
        
        private void UpdateCurrentInterfaceTable(Table? value)
        {
            // possible expansion
            if (value.Name == "Admin_accounts")
            {
                SelectedTableToList = _database.AdministratorAccounts.Local.ToBindingList();
                ExpressionLabel = "Selecting by Login";
            }
            else if (value.Name == "Publishers")
            {
                SelectedTableToList = _database.Publishers.Local.ToBindingList();
                ExpressionLabel = "Selecting by Full_name";
            }
            else if (value.Name == "Publisher_accounts")
            {
                SelectedTableToList = _database.PublisherAccounts.Local.ToBindingList();
                ExpressionLabel = "Selecting by Login";
            }
            else if (value.Name == "Verifications")
            {
                SelectedTableToList = _database.Verifications.Local.ToBindingList();
                ExpressionLabel = "Selecting by Employee_full_name";
            }
            else if (value.Name == "Developers")
            {
                SelectedTableToList = _database.Developers.Local.ToBindingList();
                ExpressionLabel = "Selecting by Full_name";
            }
            else if (value.Name == "Developer_accounts")
            {
                SelectedTableToList = _database.DeveloperAccounts.Local.ToBindingList();
                ExpressionLabel = "Selecting by Login";
            }
            else if (value.Name == "Customers")
            {
                SelectedTableToList = _database.Customers.Local.ToBindingList();
                ExpressionLabel = "Selecting by Customer_id";
            }
            else if (value.Name == "Contracts")
            {
                SelectedTableToList = _database.Contracts.Local.ToBindingList();
                ExpressionLabel = "Selecting by Contract_id";
            }
            else if (value.Name == "Projects")
            {
                SelectedTableToList = _database.Projects.Local.ToBindingList();
                ExpressionLabel = "Selecting by Project_id";
            }
            else if (value.Name == "Repositories")
            {
                SelectedTableToList = _database.Repositories.Local.ToBindingList();
                ExpressionLabel = "Selecting by Repository_id";
            }
            else if (value.Name == "Development_teams")
            {
                SelectedTableToList = _database.DevelopmentTeams.Local.ToBindingList();
                ExpressionLabel = "Selecting by Team_member_id";
            }
        }
    }
}
