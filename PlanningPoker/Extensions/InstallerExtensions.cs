
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanningPoker.Common;

namespace PlanningPoker.WebApi.Extensions
{
    public static class InstallerExtensions
    {
        public static void InstallServiceAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(Installers.IInstaller)
              .IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface).Select(Activator.CreateInstance)
              .Cast<Installers.IInstaller>().ToList();
            installers.ForEach(x =>
            { x.InstallerService(services, configuration); });
        }

    }

    public static class DbConnectionStringExtensions
    {
        public static string DbConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(AppConstants.AppConfig.ConnectionString);
        }
    }
}
