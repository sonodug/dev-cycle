using System.Collections.ObjectModel;
using System.Windows.Controls;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Pages.Admin;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle.ViewModel
{
    public class MenuViewModel : ObservableObject
    {
        public ObservableCollection<MenuModel> MenuItems { get; set; }

        private MenuModel _selectedMenuItem;

        public MenuModel SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;

                OnPropertyChanged();

                if (_selectedMenuItem != null)
                {
                    _pageService.ChangePage(_selectedMenuItem.TargetPage);
                }
            }
        }

        private readonly PageService _pageService;

        public Page PageSource { get; set; }

        public MenuViewModel(PageService pageService)
        {
            CreateMenuItems();

            _pageService = pageService;

            _pageService.PageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new HomePage());
        }

        public void CreateMenuItems()
        {
            MenuItems = new ObservableCollection<MenuModel>();

            MenuItems.Add(new MenuModel
            {
                MenuName = "Home",
                MenuColor = "#FF5733",
                TargetPage = new HomePage()
            });
            
            MenuItems.Add(new MenuModel
            {
                MenuName = "Table management",
                MenuColor = "#ae2012",
                TargetPage = new TablesPage()
            });

            MenuItems.Add(new MenuModel
            {
                MenuName = "Filters",
                MenuColor = "#00b4d8",
                TargetPage = new FiltersPage()
            });
        }
    }    
}
