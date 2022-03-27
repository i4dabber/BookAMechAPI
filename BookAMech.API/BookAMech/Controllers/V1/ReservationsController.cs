using BookAMech.Contracts.V1;
using BookAMech.Contracts.V1.Requests;
using BookAMech.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookAMech.Controllers.V1
{
    public class ReservationsController : Controller
    {
        private List<Reservation> _reservations;

        public ReservationsController()
        {
            _reservations = new List<Reservation>();
            for (var i = 0; i < 5; i++)
            {
               _reservations.Add(new Reservation
               {
                   Id = Guid.NewGuid().ToString()
               });    
            }
        }

        [HttpGet(ApiRoutes.Reservations.GetAll)]
        public IActionResult GetAllReservations()
        {
            return Ok(_reservations);
        }

        [HttpPost(ApiRoutes.Reservations.Create)]
        public IActionResult CreateReservation([FromBody] CreateReservationRequest reservationRequest)
        {

            var reservation = new Reservation { Id = reservationRequest.Id };

            if(string.IsNullOrEmpty(reservation.Id))
                reservation.Id = Guid.NewGuid().ToString();

            _reservations.Add(reservation);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Reservations.Get.Replace("{reservationId", reservation.Id);
            return Created(locationUri, reservation);
        }
    }
}
