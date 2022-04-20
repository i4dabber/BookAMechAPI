using BookAMech.Data;
using BookAMech.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Following repository pattern
namespace BookAMech.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext _context;

        public ReservationService(DataContext context)
        {
            _context = context; 
        }

        public async Task<List<Reservation>> GetAllReservationAsync()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(Guid reservationId)
        {
            return await _context.Reservations.SingleOrDefaultAsync(x => x.Id == reservationId);
        }

        public async Task<bool> CreateReservationAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            var created = await _context.SaveChangesAsync();
            return created > 0;

        }

        public async Task<bool> UpdateReservationAsync(Reservation reservationToUpdate)
        {
            _context.Reservations.Update(reservationToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0; // IF WE HAVE AN UPDATED ITEM RETURN TRUE OR FALSE
        }

        public async Task<bool> DeleteReservationAsync(Guid reservationId)
        {
            var reservation = await GetReservationByIdAsync(reservationId);

            if(reservation == null)
                return false;
            
            _context.Reservations.Remove(reservation);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> userOwnReservationAsync(Guid reservationId, string userId)
        {
            //AsNoTracking() avoid conflict while we are doing CRUD
            var reservation = await _context.Reservations.AsNoTracking().SingleOrDefaultAsync(x => x.Id == reservationId);

            if(reservation == null)
            {
                return false;
            }
           
            if(reservation.UserId != userId )
            {
                return false;
            }

            return true;
        }
    }
}
