using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace wpf_game_dev_cycle.Services
{
    public class WindowNavigationService
    {
        public event Action<Window> WindowChanged = (window) =>
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
            window.Show();
        };
        
        public void ChangeWindow(Window window) => WindowChanged?.Invoke(window);
    }
}