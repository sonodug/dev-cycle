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
                    _pageServiceFirstNest.ChangePage(_selectedMenuItem.TargetPage);
                }
            }
        }

        private readonly PageServiceFirstNest _pageServiceFirstNest;

        public Page PageSource { get; set; }

        public MenuViewModel(PageServiceFirstNest pageServiceFirstNest)
        {
            CreateMenuItems();

            _pageServiceFirstNest = pageServiceFirstNest;

            _pageServiceFirstNest.PageChanged += (page) => PageSource = page;
            _pageServiceFirstNest.ChangePage(new HomePage());
        }

        public void CreateMenuItems()
        {
            MenuItems = new ObservableCollection<MenuModel>();

            MenuItems.Add(new MenuModel
            {
                MenuName = "Home",
                TargetPage = new HomePage()
            });
            
            MenuItems.Add(new MenuModel
            {
                MenuName = "Table management",
                TargetPage = new TablesPage()
            });

            MenuItems.Add(new MenuModel
            {
                MenuName = "Filters",
                TargetPage = new FiltersPage()
            });
        }
    }    
}
