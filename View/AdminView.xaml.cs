using System.Windows;
using System.Windows.Input;

namespace wpf_game_dev_cycle.View;

public partial class AdminView : Window
{
    public AdminView()
    {
        InitializeComponent();
    }
    
    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
    
    private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.MainWindow.WindowState = WindowState.Minimized;
    }

    private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
    {
        if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
        else
            Application.Current.MainWindow.WindowState = WindowState.Normal;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}