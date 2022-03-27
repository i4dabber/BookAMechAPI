using BookAMech.Domain;
using System;
using System.Collections.Generic;

namespace BookAMech.Services
{
    public interface IReservationService
    {
        List<Reservation> GetAllReservation();

        Reservation GetReservationById(Guid id);
    }
}
