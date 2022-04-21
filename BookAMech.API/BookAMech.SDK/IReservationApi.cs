using BookAMech.Contracts.V1.Requests;
using BookAMech.Contracts.V1.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//Refit is creating its own interface just by describing attritubes. Meaning interface methods can generate everything for us.
namespace BookAMech.SDK
{
    [Headers("Authorization: Bearer")]
    public interface IReservationApi
    {
        [Get("/api/v1/reservations")]
        Task<ApiResponse<List<ReservationResponse>>> GetAllReservationAsync();

        [Get("/api/v1/reservations/{reservationId}")]
        Task<ApiResponse<ReservationResponse>> GetReservationAsync(Guid reservationId);

        [Post("/api/v1/reservations")]
        Task<ApiResponse<ReservationResponse>> CreateReservationAsync([Body] CreateReservationRequest createReservationRequest);

        [Put("/api/v1/reservations")]
        Task<ApiResponse<ReservationResponse>> UpdateReservationAsync(Guid reservationId ,[Body] UpdateReservationRequest createReservationRequest);

        [Delete("/api/v1/reservations")]
        Task<ApiResponse<string>> DeleteReservationAsync(Guid reservationId);
    }
}
