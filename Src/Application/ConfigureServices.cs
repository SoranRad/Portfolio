using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using WebUI.Configuration;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // add mapster
            services.AddMapster(assembly);

            // add mediatR
            services.AddMediatR(assembly);

            // fluentValidation
            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.DefaultClassLevelCascadeMode    = CascadeMode.Stop;
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode     = CascadeMode.Stop;

            // add pipeline


            // add other services

            return services;
        }



    }
}