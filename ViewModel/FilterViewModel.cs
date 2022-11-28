using System.Collections.ObjectModel;
using System.Windows.Controls;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Pages.Filters;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle.ViewModel
{
    public class FilterViewModel : ObservableObject
    {
        private NewPageService _pageService;
        public ObservableCollection<FilterModel> FilterItems { get; set; }

        private FilterModel _selectedFilterItem;
        
        public Page PageSource { get; set; }

        public FilterModel SelectedFilterItem
        {
            get => _selectedFilterItem;
            set
            {
                _selectedFilterItem = value;
                OnPropertyChanged();
                if (_selectedFilterItem != null)
                    _pageService.ChangePage(_selectedFilterItem.TargetPage);
            }
        }

        public FilterViewModel(NewPageService pageService)
        {
            CreateFilterItems();
            _pageService = pageService;
            _pageService.PageChanged += (page) => PageSource = page;
        }

        public void CreateFilterItems()
        {
            FilterItems = new ObservableCollection<FilterModel>();

            FilterItems.Add(new FilterModel
            {
                FilterName = "Authentication parameters filter",
                TargetPage = new AuthFilterPage()
            });
            
            FilterItems.Add(new FilterModel
            {
                FilterName = "Registration date filter",
                TargetPage = new RegDateFilterPage()
            });
        }
    }    
}
