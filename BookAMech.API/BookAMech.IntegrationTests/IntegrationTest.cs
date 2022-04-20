using BookAMech.Contracts.V1;
using BookAMech.Contracts.V1.Requests;
using BookAMech.Contracts.V1.Responses;
using BookAMech.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

namespace BookAMech.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _client;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }
                        //services.RemoveAll(typeof(DbContextOptions<DataContext>));
                        services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });

            _client = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        public async Task<ReservationResponse> CreatePostAsync(CreateReservationRequest request)
        {
            var response = await _client.PostAsJsonAsync(ApiRoutes.Reservations.Create, request);
            return await response.Content.ReadAsAsync<ReservationResponse>();
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await _client.PostAsJsonAsync(ApiRoutes.Users.Register, new UserRegistrationRequest
            {
                Email = "test@integration.com",
                Password = "SomePass1234!",
                Phonenumber = 0
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }
    }
}
