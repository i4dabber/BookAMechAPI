using BookAMech.Installers.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookAMech.Extensions
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers =
                typeof(Startup).Assembly.ExportedTypes.Where //Select all installers and make an instance of them, and cast them as an interface
                (t => typeof(IInstaller).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();


            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
