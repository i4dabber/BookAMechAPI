using BookAMech.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAMech.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllReservationAsync();

        Task<Reservation> GetReservationByIdAsync(Guid Id);

        Task<bool> CreateReservationAsync(Reservation reservation);
     
        Task<bool> UpdateReservationAsync(Reservation reservationToUpdate);

        Task<bool> DeleteReservationAsync(Guid Id);
    }
}
