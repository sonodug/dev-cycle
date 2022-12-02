using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Pages.Filters;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle.ViewModel
{
    public class FilterViewModel : ObservableObject
    {
        public ObservableCollection<FilterModel> FilterItems { get; set; }
        private PageServiceSecondNest _pageServiceSecondNest;
        private UpdateTableService _updateTableService;

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
                    _pageServiceSecondNest.ChangePage(_selectedFilterItem.TargetPage);
            }
        }

        public FilterViewModel(PageServiceSecondNest pageServiceSecondNest, UpdateTableService updateTableService)
        {
            CreateFilterItems();
            _pageServiceSecondNest = pageServiceSecondNest;
            _pageServiceSecondNest.PageChanged += (page) => PageSource = page;

            _updateTableService = updateTableService;
            _updateTableService.TableUpdated += UpdateTables;
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
            
            FilterItems.Add(new FilterModel
            {
                FilterName = "Contract price filter",
                TargetPage = new ContractPriceFilterPage()
            });
            FilterItems.Add(new FilterModel
            {
                FilterName = "Dev team filter",
                TargetPage = new ContractPriceFilterPage()
            });
            FilterItems.Add(new FilterModel
            {
                FilterName = "Work status filter",
                TargetPage = new WorkStatusFilterPage()
            });
        }

        private void UpdateTables()
        {
            FilterItems[0].TargetPage = new AuthFilterPage();
            FilterItems[1].TargetPage = new RegDateFilterPage();
            FilterItems[2].TargetPage = new ContractPriceFilterPage();
            FilterItems[3].TargetPage = new ContractPriceFilterPage();
            _pageServiceSecondNest.ChangePage(new WorkStatusFilterPage());
            //page matching
        }
    }    
}
