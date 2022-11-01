using Microsoft.Extensions.DependencyInjection;
using wpf_game_dev_cycle.Services;
using wpf_game_dev_cycle.ViewModel;

namespace wpf_game_dev_cycle
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddSingleton<WindowNavigationService>();
            
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddSingleton<MainViewModel>();
            
            services.AddSingleton<PageService>();
            services.AddSingleton<LoginService>();
            services.AddSingleton<RegistrationService>();

            _provider = services.BuildServiceProvider();

            foreach (var service in services)
            {
                _provider.GetRequiredService(service.ServiceType);
            }
        }

        public LoginViewModel LoginViewModel => _provider.GetRequiredService<LoginViewModel>();
        public RegisterViewModel RegisterViewModel => _provider.GetRequiredService<RegisterViewModel>();
        public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();
    }
}