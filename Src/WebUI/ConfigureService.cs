using System.Reflection;
using System.Text.Json;
using Autofac;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using WebUI.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace WebUI
{
    public static class ConfigureService
    {

        public static IServiceCollection AddWebUIServices
            (
                this IServiceCollection Services,
                WebApplicationBuilder        
                    builder
            )
        {
            // AutoFact
            builder.Host.AddAutoFact();

            // Serilog
            builder.Host.AddSeriLog();

            // Configuration File
            ConfigurationManager configuration = builder.Configuration;

            // Settings
            Services.AddSiteConfiguration(configuration);

            // Antiforgery
            Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            // ForwardedHeaders
            Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            // HttpContext
            Services.AddHttpContextAccessor();

            // Caching
            Services.AddDistributedMemoryCache();

            // WebMurkupMin
            Services.AddWebMarkUpMin();

            // RazorPages
            builder.Services
                .AddRazorPages() 
                .AddRazorRuntimeCompilation()
                .AddJsonOptions(opts =>
                { 
                    opts.JsonSerializerOptions.PropertyNamingPolicy = null;
                    opts.JsonSerializerOptions.PropertyNameCaseInsensitive  = false;
                })
                .AddRazorPagesOptions(opt =>
                {
                    opt.Conventions.AddPageRoute("/Home/Index", "");
                })
                .AddSessionStateTempDataProvider()
                ;



            return Services;
        }


        public static void  ConfigureAutofact(this IHostBuilder Host,params Assembly[] assembly)
        {

            Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.ConfigureDelegate(assembly);

                builder.RegisterSeriLog();

            });
        }
    }
}
