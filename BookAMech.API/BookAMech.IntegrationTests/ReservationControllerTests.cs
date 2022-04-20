using BookAMech.Contracts.V1;
using BookAMech.Contracts.V1.Requests;
using BookAMech.Contracts.V1.Responses;
using BookAMech.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace BookAMech.IntegrationTests
{
    public class ReservationControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAllReservatíon_ReturnEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await _client.GetAsync(ApiRoutes.Reservations.GetAll);

            // Assert   
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Reservation>>()).ShouldBeEmpty();
        }
        //[Fact]
        //public async Task Get_Reservation_WhenExistsInDatabase()
        //{
        //    // Arrange
        //    await AuthenticateAsync();
        //    var createdReservation = await CreatePostAsync(new CreateReservationRequest 
        //    {
        //        CustomerName = "Berat",
        //        CompanyName = "Google",
        //        StreetAddress = "Danmarksgade",
        //        StreetNumber = 12,
        //        Phonenumber = 0,
        //        startDate = DateTime.UtcNow
        //    });

        //    // Act
        //    var response = await _client.GetAsync(ApiRoutes.Reservations.Get.Replace("{reservationId}", createdReservation.Id.ToString()));

        //    // Assert
        //    response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        //    var returnedPost = await response.Content.ReadAsAsync<Reservation>();
        //    //returnedPost.Id.ShouldBe(createdReservation.Id);
        //    returnedPost.CustomerName.ShouldBe("Berat");
        //    returnedPost.CompanyName.ShouldBe("Google");
        //    returnedPost.StreetAddress.ShouldBe("Danmarksgade");
        //    returnedPost.StreetNumber.ShouldBe(12);
        //    returnedPost.Phonenumber.ShouldBe(0);
        //    returnedPost.startDate.ShouldBe(DateTime.UtcNow);
        //}
    }
}
