using BookAMech.Installers.Interface;
using BookAMech.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookAMech.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //Service registration

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IReservationService, ReservationService>();

            services.AddAutoMapper(typeof(Startup));

        }
    }
    
}
