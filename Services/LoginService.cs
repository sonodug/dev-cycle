using System;

namespace wpf_game_dev_cycle.Services;

public class LoginService
{
    public event Action Logged;
    public void Login() => Logged?.Invoke();
}