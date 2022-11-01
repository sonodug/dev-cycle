using System;

namespace wpf_game_dev_cycle.Services
{
    public class RegistrationService
    {
        public event Action Registered;
        public void Register() => Registered?.Invoke();
    }
}