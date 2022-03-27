using BookAMech.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookAMech.Services
{
    public class ReservationService : IReservationService
    {
        private readonly List<Reservation> _reservations;

        public ReservationService()
        {
            _reservations = new List<Reservation>();
            for (var i = 0; i < 5; i++)
            {
                _reservations.Add(new Reservation
                {
                    Id = Guid.NewGuid(),
                    Name = $"Test name {i}"
                });
            }
        }
        public List<Reservation> GetAllReservation()
        {
            return _reservations;
        }

        public Reservation GetReservationById(Guid reservationId)
        {
            return _reservations.SingleOrDefault(x => x.Id == reservationId);
        }
    }
}
