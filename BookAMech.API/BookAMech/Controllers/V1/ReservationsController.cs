using BookAMech.Contracts.V1;
using BookAMech.Contracts.V1.Requests;
using BookAMech.Contracts.V1.Responses;
using BookAMech.Domain;
using BookAMech.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAMech.Extensions;

namespace BookAMech.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Everything accessed for this controller needs a valid token
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;   
        }

        [HttpGet(ApiRoutes.Reservations.GetAll)]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            return Ok(await _reservationService.GetAllReservationAsync());
        }

        [HttpGet(ApiRoutes.Reservations.Get)]
        public async Task<IActionResult> GetReservationAsync([FromRoute] Guid reservationId)
        {

            var userOwnReservation = await _reservationService.userOwnReservationAsync(reservationId, HttpContext.GetUserId());

            //Check for user owns reservation
            if (!userOwnReservation)
            {
                return BadRequest(new { error = "You do not own this reservation" });
            }

            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpPut(ApiRoutes.Reservations.Update)]
        public async Task<IActionResult> UpdateReservationAsync([FromRoute]Guid reservationId, [FromBody] UpdateReservationRequest request)
        {

            var userOwnReservation = await _reservationService.userOwnReservationAsync(reservationId, HttpContext.GetUserId());

            //Check for user owns reservation
            if (!userOwnReservation)
            {
                return BadRequest(new { error = "You do not own this reservation" });
            }

            var reservation = new Reservation
            {
                Id = reservationId,
                CustomerName = request.CustomerName,
                CompanyName = request.CompanyName,
                StreetAddress = request.StreetAddress,
                StreetNumber = request.StreetNumber,
                Phonenumber = request.Phonenumber       
            };    

            var updated = await _reservationService.UpdateReservationAsync(reservation);  

            if(updated)
                return Ok(reservation);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Reservations.Delete)]
        public async Task<IActionResult> DeleteReservationAsync([FromRoute] Guid reservationId)
        {
            var userOwnReservation = await _reservationService.userOwnReservationAsync(reservationId, HttpContext.GetUserId());

            //Check for user owns reservation
            if (!userOwnReservation)
            {
                return BadRequest(new { error = "You do not own this reservation" });
            }

            var deleted = await _reservationService.DeleteReservationAsync(reservationId);

            if (deleted)
                return NoContent();

            return NotFound();
        }


        [HttpPost(ApiRoutes.Reservations.Create)]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest reservationRequest)
        {

            var reservation = new Reservation
            {
                CustomerName = reservationRequest.CustomerName,
                CompanyName = reservationRequest.CompanyName,
                StreetAddress = reservationRequest.StreetAddress,
                StreetNumber = reservationRequest.StreetNumber,
                Phonenumber = reservationRequest.Phonenumber,
                startDate = reservationRequest.startDate,
                UserId = HttpContext.GetUserId()
            };

          

            await _reservationService.CreateReservationAsync(reservation);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Reservations.Get.Replace("{reservationId}", reservation.Id.ToString());

            var response = new ReservationResponse { Id = reservation.Id };
            return Created(locationUri, response);
        }
    }
}
