using BookAMech.Contracts.V1;
using BookAMech.Contracts.V1.Requests;
using BookAMech.Contracts.V1.Responses;
using BookAMech.Domain;
using BookAMech.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookAMech.Controllers.V1
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;   
        }

        [HttpGet(ApiRoutes.Reservations.GetAll)]
        public IActionResult GetAllReservations()
        {
            return Ok(_reservationService.GetAllReservation());
        }

        [HttpGet(ApiRoutes.Reservations.Get)]
        public IActionResult GetReservation([FromRoute]Guid reservationId)
        {
            var reservation = _reservationService.GetReservationById(reservationId);

            if(reservation == null)
                return NotFound();  

            return Ok(reservation);
        }


        [HttpPost(ApiRoutes.Reservations.Create)]
        public IActionResult CreateReservation([FromBody] CreateReservationRequest reservationRequest)
        {

            var reservation = new Reservation { Id = reservationRequest.Id };

            if(reservation.Id != Guid.Empty)
                reservation.Id = Guid.NewGuid();

            _reservationService.GetAllReservation().Add(reservation);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Reservations.Get.Replace("{reservationId}", reservation.Id.ToString());

            var response = new ReservationResponse { Id = reservation.Id };
            return Created(locationUri, response);
        }
    }
}
