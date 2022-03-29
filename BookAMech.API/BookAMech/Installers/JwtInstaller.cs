using BookAMech.Installers.Interface;
using BookAMech.Services;
using BookAMech.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookAMech.Installers
{
    public class JwtInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings) ,jwtSettings); //Mapping to appsetting.json Secret objekt
            services.AddSingleton(jwtSettings);


            services.AddScoped<IUserService, UserService>();

            //Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x => //Bearer token validation
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true, //Used to check for correct secret
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)), //Encodes the secret since its in binary
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    }; 
                });
        }
    }
}
