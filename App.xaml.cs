using System.Windows;
using wpf_game_dev_cycle.View;

namespace wpf_game_dev_cycle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            ViewModelLocator.Init();

            //var navigationService = new WindowNavigationService();
            var loginView = new LoginView();
            //navigationService.CurrentWindow = loginView;
            
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainWindow();
                    
                    mainView.Show();
                    loginView.Close();
                }
            };
        }
    }
}
