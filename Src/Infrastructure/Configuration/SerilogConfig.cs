using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using Serilog.Events;

namespace Infrastructure.Configuration
{
    public static class SerilogConfig
    {
        public static void CreateLogger()
        {
            Log.Logger = CreateLoggerConfiguration();
        }

        private static Serilog.Core.Logger CreateLoggerConfiguration(string Path = "logs/log-.txt")
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
                        buffered: true//,
                                      //shared: true
                                      //flushToDiskInterval: TimeSpan.FromSeconds(1)
                    )
                )
                .CreateLogger();
        }

        public static void RegisterSeriLogToAutoFac(this ContainerBuilder container)
        {
            container.Register<ILogger>((c, p) => CreateLoggerConfiguration()).SingleInstance();
        }
    }

}
