﻿using BookAMech.Contracts.V1;
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
using AutoMapper;

namespace BookAMech.Controllers.V1
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Everything accessed for this controller needs a valid token
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Reservations.GetAll)]
        public async Task<IActionResult> GetAllReservationsAsync()
        {

            var reservations = await _reservationService.GetAllReservationAsync();
            var reservationResponse = _mapper.Map<List<ReservationResponse>>(reservations);
            return Ok(reservationResponse);
        }
  

        [HttpGet(ApiRoutes.Reservations.Get)]
        public async Task<IActionResult> GetReservationAsync([FromRoute] Guid reservationId)
        {
            //Check for user owns reservation - Make user its user specific content
            var userOwnReservation = await _reservationService.userOwnReservationAsync(reservationId, HttpContext.GetUserId());
          
            if (!userOwnReservation)
            {
                return BadRequest(new { error = "You do not own this reservation" });
            }

            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);

            if (reservation == null)
                return NotFound();

            return Ok(_mapper.Map<ReservationResponse>(reservation));
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

            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            reservation.CustomerName = request.CustomerName;
            reservation.CompanyName = request.CompanyName;
            reservation.StreetAddress = request.StreetAddress;
            reservation.StreetNumber = request.StreetNumber;
            reservation.Phonenumber = request.Phonenumber;
            reservation.startDate = DateTime.UtcNow;
            reservation.UserId = HttpContext.GetUserId();

            var updated = await _reservationService.UpdateReservationAsync(reservation);  

            if(updated)
                return Ok(_mapper.Map<UpdateReservationRequest>(reservation));

            return NotFound(); 

        }

        /// <summary>
        /// Deletes a specific Reservation.
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
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

            var response = _mapper.Map<CreateReservationRequest>(reservation);
            return Created(locationUri, response);
        }
    }
}
