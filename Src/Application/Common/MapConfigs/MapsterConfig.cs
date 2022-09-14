using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace WebUI.Configuration
{
    public static class MapsterConfig
    {
        public static TypeAdapterConfig AddMapster(this IServiceCollection Services, params Assembly[] assemblies)
        {

            var Config = GetConfiguredMappingConfig1();

            // scaning
            Config.Scan(assemblies);

            // singleton
            Services.AddSingleton(Config);

            // servives
            Services.AddScoped<IMapper, ServiceMapper>();

            return Config;
        }

        private static TypeAdapterConfig GetConfiguredMappingConfig1()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            TypeAdapterConfig<string, string>.Clear();
            TypeAdapterConfig.GlobalSettings.Default.AddDestinationTransform((string x) => (string.IsNullOrEmpty(x) ? null : x.Trim()));
            TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);

            config
                .Default
                .AddDestinationTransform((string x) => (string.IsNullOrEmpty(x) ? null : x.Trim()));

            config
                .When((srcType, destType, mapType) => srcType == destType)
                .Ignore("Id");



            return config;
        }
    }
}
