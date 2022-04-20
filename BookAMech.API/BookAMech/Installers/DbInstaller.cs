using BookAMech.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookAMech.Data;
using BookAMech.Installers.Interface;

namespace BookAMech.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();

            //Add scoped means lifetime of Iservice and real service is the same, to track them

            //Service registration
            //services.AddScoped<IReservationService, ReservationService>();
        }
    }
}
