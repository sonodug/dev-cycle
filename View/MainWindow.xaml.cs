using System.Data.Entity;
using System.Windows;
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
            db.Accounts.Load(); // загружаем данные
            accounts.ItemsSource = db.Accounts.Local.ToBindingList(); // устанавливаем привязку к кэшу
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
