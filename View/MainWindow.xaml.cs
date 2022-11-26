using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using wpf_game_dev_cycle.Model;
using wpf_game_dev_cycle.Services;

namespace wpf_game_dev_cycle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        // private void returnSelectByIdButton_Click(object sender, RoutedEventArgs e)
        // {
        //     var ad_accounts = db.AdministratorAccounts;
        //     accounts.ItemsSource = ad_accounts.ToList();
        // }
        //
    }
}
