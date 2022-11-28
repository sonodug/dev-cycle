using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle.ViewModel
{
    public class FilterPagesViewModel : ObservableObject
    {
        private CompanyContext _database;
        private IEnumerable _authTableInfo;
        private IEnumerable _regDateTable;
            
        private NewPageService _pageService;
        private Page _pageSource;

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
        
        public FilterPagesViewModel(NewPageService pageService)
        {
            _database = new CompanyContext();
            
            var filter1 = InitAuthFilter();
            AuthTableInfo = filter1;

            var filter2 = InitRegFilter();
            RegDateTable = filter2;

            _pageService = pageService;
            _pageService.PageChanged += (page) => PageSource = page;
        }

        private IEnumerable InitAuthFilter()
        {
            _database.PublisherAccounts.Load();
            _database.DeveloperAccounts.Load();
            var filter = 
                _database.PublisherAccounts.Select(i => new { i.Login, i.Password })
                .Union(_database.DeveloperAccounts.Select(i => new { i.Login, i.Password }));
            return filter.ToList();;
        }
        
        private IEnumerable InitRegFilter()
        {
            _database.Developers.Load();
            var filter = 
                _database.Developers.Select(i => new { i.Employee_code, i.Employment_date })
                .OrderBy(i => i.Employment_date);
            return filter.ToList();;
        }
    }
}