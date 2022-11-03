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
            Window cur = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            window.Show();
            cur.Close();
        };
        
        public void ChangeWindow(Window window) => WindowChanged?.Invoke(window);
    }
}