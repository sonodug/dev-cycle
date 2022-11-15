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
        
        // private void selectByIdButton_Click(object sender, RoutedEventArgs e)
        // {
        //     using (var context = new CompanyContext())
        //     {
        //         int id;
        //         if (int.TryParse(textbox.Text, out id))
        //         {
        //             var ad_accounts = db.AdministratorAccounts.Where(p=> p.Account_id == id);
        //             accounts.ItemsSource = ad_accounts.ToList();
        //         }
        //         else
        //             return;
        //     }
        // }
        //
        // private void returnSelectByIdButton_Click(object sender, RoutedEventArgs e)
        // {
        //     var ad_accounts = db.AdministratorAccounts;
        //     accounts.ItemsSource = ad_accounts.ToList();
        // }
        //
    }
}
