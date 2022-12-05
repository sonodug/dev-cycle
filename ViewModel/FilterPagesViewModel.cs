using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Pages.Filters;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle.ViewModel
{
    public class FilterPagesViewModel : ObservableObject
    {
        private CompanyContext _database;
        
        private IEnumerable _authTableInfo;
        private IEnumerable _regDateTable;
        private IEnumerable _contractPriceTable;
        private IEnumerable _workStatusTable;
        private IEnumerable _projectStatusTable;
        private IEnumerable _verificationTable;
            
        private PageServiceSecondNest _pageServiceSecondNest;
        private UpdateTableService _updateTableService;
        private Page _pageSource;
        public ICommand UpdateFiltersCommand { get; }

        public Page PageSource
        {
            get { return _pageSource; }
            set
            {
                _pageSource = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable AuthTableInfo
        {
            get => _authTableInfo;
            set
            {
                _authTableInfo = value;
                OnPropertyChanged();
            }
        }
        
        public IEnumerable RegDateTable
        {
            get => _regDateTable;
            set
            {
                _regDateTable = value;
                OnPropertyChanged();
            }
        }
        
        public IEnumerable ContractPriceTable
        {
            get => _contractPriceTable;
            set
            {
                _contractPriceTable = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable WorkStatusTable
        {
            get => _workStatusTable;
            set
            {
                _workStatusTable = value;
                OnPropertyChanged();
            }
        }
        
        public IEnumerable ProjectStatusTable
        {
            get => _projectStatusTable;
            set
            {
                _projectStatusTable = value;
                OnPropertyChanged();
            }
        }
        
        public IEnumerable VerificationTable
        {
            get => _verificationTable;
            set
            {
                _verificationTable = value;
                OnPropertyChanged();
            }
        }

        public FilterPagesViewModel(PageServiceSecondNest pageServiceSecondNest, UpdateTableService updateTableService)
        {
            _database = new CompanyContext();
            
            InitializeFilters();

            UpdateFiltersCommand = new RelayCommand(ExecuteUpdateFiltersCommand);
            
            _pageServiceSecondNest = pageServiceSecondNest;
            _pageServiceSecondNest.PageChanged += (page) => PageSource = page;

            _updateTableService = updateTableService;
        }

        private void InitializeFilters()
        {
            var filter1 = InitAuthFilter();
            AuthTableInfo = filter1;

            var filter2 = InitRegFilter();
            RegDateTable = filter2;

            var filter3 = InitContractPriceFilter();
            ContractPriceTable = filter3;

            var filter4 = InitWorkStatusFilter();
            WorkStatusTable = filter4;
            
            var filter5 = InitProjectStatusFilter();
            ProjectStatusTable = filter5;
            
            var filter6 = InitVerificationFilter();
            VerificationTable = filter6;
        }

        private IEnumerable InitAuthFilter()
        {
            _database.PublisherAccounts.Load();
            _database.DeveloperAccounts.Load();
            var filter = 
                _database.PublisherAccounts.Select(pa => new
                        {
                            Name = _database.Publishers.Where(p => p.Publisher_code == pa.Publisher_code)
                                .Select(p => p.Full_name).FirstOrDefault(),
                            pa.Login,
                            pa.Password,
                        })
                .Union(_database.DeveloperAccounts.Select(da => new 
                    {
                        Name = _database.Developers.Where(d => d.Employee_code == da.Employee_code)
                            .Select(d => d.Full_name).FirstOrDefault(),
                        da.Login, 
                        da.Password 
                    }));
            return filter.ToList();
        }
        
        private IEnumerable InitRegFilter()
        {
            _database.Developers.Load();
            var filter = 
                _database.Developers.Select(i => new
                    {
                        i.Full_name,
                        i.Employment_date
                    })
                .OrderBy(i => i.Employment_date);
            return filter.ToList();
        }
        
        private IEnumerable InitContractPriceFilter()
        {
            _database.Contracts.Load();
            var count = _database.Contracts.Count();
            var sum = _database.Contracts.Sum(contract => contract.Price);
            var min = _database.Contracts.Min(contract => contract.Price);
            var max = _database.Contracts.Max(contract => contract.Price);
            var avg = _database.Contracts.Average(contract => contract.Price);
            var filter = CreateList(new
            {
                ContractCount = count,
                TotalPrice = sum,
                MinPrice = min,
                MaxPrice = max,
                AvgPrice = avg
            });
            return filter;
        }
        
        private IEnumerable InitWorkStatusFilter()
        {
            _database.DevelopmentTeams.Load();
            _database.Developers.Load();
            _database.Repositories.Load();
            _database.Projects.Load();
            
            var filter =
                _database.DevelopmentTeams.Select(dt => new
                {
                    dt.Work_status,
                    DeveloperName = _database.Developers.Where(d => d.Employee_code == dt.Employee_code)
                        .Select(d => d.Full_name).FirstOrDefault(),

                    ProjectName = _database.Repositories.Select(r => new
                    {
                        Project = _database.Projects.Where(pp => dt.Repository_id == r.Repository_id)
                            .Where(pp => pp.Project_id == r.Project_id).Select(pp => pp.Name)
                            .FirstOrDefault()
                    }).FirstOrDefault(k => k.Project != null)
                });
            
            return filter.ToList();
        }

        private IEnumerable InitProjectStatusFilter()
        {
            _database.Projects.Load();
            var filter = 
                _database.Projects.Select(p => new
                    {
                        p.Name,
                        p.Status,
                        Deadline = p.Deadline_date
                    })
                    .OrderBy(p => p.Deadline);
            return filter.ToList();
        }
        
        private IEnumerable InitVerificationFilter()
        {
            _database.Verifications.Load();
            _database.Publishers.Load();
            var filter =
                _database.Verifications.Select(v => new
                {
                    VerificationCode = v.Verification_code, 
                    Employee = v.Employee_full_name,
                    Publisher = _database.Publishers.Where(p => p.Publisher_code == v.Publisher_code)
                        .Select(p => p.Full_name).FirstOrDefault()
                });
            return filter.ToList();
        }
        
        public static IEnumerable<T> CreateList<T>(params T[] elements)
        {
            return new List<T>(elements);
        }
        
        private void ExecuteUpdateFiltersCommand(object obj)
        {
            _database.SaveChanges();
            InitializeFilters();
            _updateTableService.Update();
        }
    }
}