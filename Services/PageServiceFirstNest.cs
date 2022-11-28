using System;
using System.Windows.Controls;

namespace wpf_game_dev_cycle.Services
{
    public class PageServiceFirstNest
    {
        public event Action<Page> PageChanged;
        public void ChangePage(Page page) => PageChanged?.Invoke(page);
    }
}