using System;
using System.Windows;
using Microsoft.Extensions.Logging;
using Serilog;
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

            // var adminView = new AdminView();
            // adminView.Show();

            var loginView = new LoginView();
            loginView.Show();

            // var mainView = new MainWindow();
            // mainView.Show();
        }
    }
}
