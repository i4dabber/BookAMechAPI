using System;

namespace BookAMech.Contracts.V1.Responses
{
    public class ReservationResponse
    {
        public Guid Id { get; set; }

        public string CustomerName { get; set; }

        public string CompanyName { get; set; }

        public string StreetAddress { get; set; }

        public int StreetNumber { get; set; }

        public int Phonenumber { get; set; }

        public DateTime startDate { get; set; }

        public string UserId { get; set; }
    }
}
