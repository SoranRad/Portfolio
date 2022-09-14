using Autofac;
using Serilog.Events;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace WebUI.Configuration
{
    public static class SerilogConfig
    {
        public static Serilog.Core.Logger CreateLoggerConfiguration(string Path = "logs/log-.txt")
        {
            return new LoggerConfiguration()
                .MinimumLevel.Warning()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(a =>
                    a.File(
                        Path,
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}{NewLine}",
                        shared: true,
                        flushToDiskInterval: TimeSpan.FromSeconds(1)
                    )
                )
                .CreateLogger();
        }

        public static void AddSeriLog(this ConfigureHostBuilder Host, string Path = "logs/log-.txt")
        {
            Host.UseSerilog((context, configuration) =>
            {
                configuration.MinimumLevel.Warning();
                configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                configuration.Enrich.FromLogContext();
                configuration.WriteTo.Async(a =>
                    a.File(
                        Path,
                        rollingInterval: RollingInterval.Day,
                        outputTemplate:
                        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}{NewLine}",
                        shared: true,
                        flushToDiskInterval: TimeSpan.FromSeconds(1)
                    )
                );
            });
        }
        public static void RegisterSeriLog(this ContainerBuilder container)
        {
            container.Register<ILogger>((c, p) => CreateLoggerConfiguration()).SingleInstance();
        }

    }
}
