using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices 
    {
        public static IServiceCollection AddInfrastructureServices
            (
                this IServiceCollection services, 
                IConfiguration          configuration
            )
        {
            // add interceptor
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            // add dbContext
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<PostContext>(options =>
                    options.UseInMemoryDatabase("PostsDb"));
            }
            else
            {
                services.AddDbContext<PostContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("SqlServer"),
                        builder => builder.MigrationsAssembly(typeof(PostContext).Assembly.FullName)));
            }

            // add Initializing
            services.AddScoped<DbContextInitialiser>();


            return services;
        }


    }
}
