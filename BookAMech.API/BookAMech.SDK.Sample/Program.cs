using BookAMech.Contracts.V1.Requests;
using Refit;
using System;
using System.Threading.Tasks;

namespace BookAMech.SDK.Sample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var cachedToken = string.Empty;

            var userApi = RestService.For<IUserApi>("https://localhost:5001");
            var reservationApi = RestService.For<IReservationApi>("https://localhost:5001", new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken) //Retriving tokens from refit settings
            });;

            // User handler
            var registerResponse = await userApi.RegisterAsync(new UserRegistrationRequest
            {
                Email = "sdktest@gmail.com",
                Password = "User123-",
                Phonenumber = 1
            });

           

            var loginResponse = await userApi.LoginAsync(new UserLoginRequest
            {
                Email = "sdktest@gmail.com",
                Password = "User123-"               
            });

            Console.WriteLine(loginResponse.Content.Token);
            cachedToken = loginResponse.Content.Token; //Using the retrieved token from refit settings

            // Reservation Mocked data to display

            var getAllReservation = await reservationApi.GetAllReservationAsync();
            Console.WriteLine(getAllReservation.Content.Count);

            var createdReservation = await reservationApi.CreateReservationAsync(new CreateReservationRequest
            {
                CustomerName = "sdk sdk",
                CompanyName = "sdk company",
                StreetAddress = "sdkvej",
                StreetNumber = 99,
                Phonenumber = 12121212,
                startDate = DateTime.UtcNow
            });

            var getReservation = await reservationApi.GetReservationAsync(createdReservation.Content.Id);

            var updateReservation = await reservationApi.UpdateReservationAsync(createdReservation.Content.Id, new UpdateReservationRequest
            {
                CustomerName = "SDK UPDATED"
            });

            var deleteReservation = await reservationApi.DeleteReservationAsync(createdReservation.Content.Id);
           
        }
    }
}
