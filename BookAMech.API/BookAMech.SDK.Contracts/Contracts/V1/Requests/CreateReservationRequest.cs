using System;

namespace BookAMech.Contracts.V1.Requests
{
    public class CreateReservationRequest
    {
        public string CustomerName { get; set; }

        public string CompanyName { get; set; }

        public string StreetAddress { get; set; }

        public int StreetNumber { get; set; }

        public int Phonenumber { get; set; }

        public DateTime startDate { get; set; }
    }
}
