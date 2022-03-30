using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Oz.Bet
{
    public static class LogHelper
    {
        public static void Init(IServiceCollection serviceCollection)
        {
            var logTemplate = "{Timestamp:dd HH:mm:ss.fff} [{Level:u3}] {Message}{NewLine}{Exception}";

            var serilogLogger = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .WriteTo.Async(a => a.Logger(l => l.WriteTo.File($"/log/{Environment.GetEnvironmentVariable("Log")}.txt", outputTemplate: logTemplate, shared: true)))
               .CreateLogger();

            Log.Logger = serilogLogger;

            serviceCollection.AddLogging();
        }

    }
}
