
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using wpf_game_dev_cycle.Model;

namespace wpf_game_dev_cycle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CompanyContext db;
        public MainWindow()
        {
            InitializeComponent();
            
            db = new CompanyContext();
            db.admin_accounts.Load(); // загружаем данные
            accounts.ItemsSource = db.admin_accounts.Local.ToBindingList(); // устанавливаем привязку к кэшу
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }
        
        private void addcolumnButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new CompanyContext()) 
            { 
                context.Database.ExecuteSqlCommand( 
                    "ALTER TABLE [company].[admin_account] ADD [email] [nvarchar](20) NULL");
            }
        }
        
        private void deletecolumnButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new CompanyContext()) 
            { 
                context.Database.ExecuteSqlCommand( 
                    "ALTER TABLE [company].[admin_account] DROP COLUMN [email]"); 
            }
        }
        
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (accounts.SelectedItems.Count > 0)
            {
                for (int i = 0; i < accounts.SelectedItems.Count; i++)
                {
                    admin_account account = accounts.SelectedItems[i] as admin_account;
                    if (account != null)
                    {
                        db.admin_accounts.Remove(account);
                    }
                }
            }
            db.SaveChanges();
        }
    }
}
