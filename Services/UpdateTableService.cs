using System;

namespace wpf_game_dev_cycle.Services
{
    public class UpdateTableService
    {
        public event Action TableUpdated;
        public void Update() => TableUpdated?.Invoke();
    }
}