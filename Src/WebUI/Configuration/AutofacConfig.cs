using Autofac.Extensions.DependencyInjection;
using Autofac;
using System.Reflection;
using Domain.SharedKernel;

namespace WebUI.Configuration
{
    public static class AutofacConfig
    {
        public static void AddAutoFact(this ConfigureHostBuilder Host)
        {
            Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }

        public static void ConfigureDelegate(this ContainerBuilder containerBuilder, params Assembly[] assemblies)
        {
            containerBuilder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder
                .RegisterAssemblyOpenGenericTypes(assemblies)
                .Where(x => x.IsAssignableTo<IGenericDependency>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        }
    }
}
