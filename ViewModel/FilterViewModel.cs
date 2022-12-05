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
                FilterName = "Project status filter",
                TargetPage = new ProjectStatusFilterPage()
            });
            FilterItems.Add(new FilterModel
            {
                FilterName = "Work status filter",
                TargetPage = new WorkStatusFilterPage()
            });
            FilterItems.Add(new FilterModel
            {
                FilterName = "Verifications filter",
                TargetPage = new VerificationFilterPage()
            });
        }

        private void UpdateTables()
        {
            if (SelectedFilterItem != null)
            {
                switch (SelectedFilterItem.FilterName)
                {
                    case "Authentication parameters filter":
                        _pageServiceSecondNest.ChangePage(new AuthFilterPage());
                        break;
                    case "Registration date filter":
                        _pageServiceSecondNest.ChangePage(new RegDateFilterPage());
                        break;
                    case "Contract price filter":
                        _pageServiceSecondNest.ChangePage(new ContractPriceFilterPage());
                        break;
                    case "Work status filter":
                        _pageServiceSecondNest.ChangePage(new WorkStatusFilterPage());
                        break;
                    case "Project status filter":
                        _pageServiceSecondNest.ChangePage(new ProjectStatusFilterPage());
                        break;
                    case "Verifications filter":
                        _pageServiceSecondNest.ChangePage(new VerificationFilterPage());
                        break;
                }
            }
        }
    }    
}
