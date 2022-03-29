using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookAMech.Installers.Interface
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
