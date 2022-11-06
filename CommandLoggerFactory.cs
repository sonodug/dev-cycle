using Microsoft.Extensions.Logging;
using Serilog;
using wpf_game_dev_cycle.ViewModel;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace wpf_game_dev_cycle
{
    public class CommandLoggerFactory : LoggerFactory
    {
        private ILoggerFactory _loggerFactory;

        public CommandLoggerFactory()
        {
            _loggerFactory = Create(builder =>
            {
                LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                    .WriteTo.File("testLogger.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Debug()
                    .MinimumLevel
                    .Override("Logging commands", Serilog.Events.LogEventLevel.Debug);

                builder.AddSerilog(loggerConfiguration.CreateLogger());
            });
        }

        public ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }
    }
}