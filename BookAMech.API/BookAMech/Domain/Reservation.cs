using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookAMech.Domain
{
    [Table("Reservations")]
    public class Reservation 
    {
       [Key]
       public Guid Id { get; set; }

       public string CustomerName { get; set; }

       public string CompanyName { get; set; }

       public string StreetAddress { get; set; }

       public int StreetNumber { get; set; }

       public int Phonenumber { get; set; }

       public DateTime startDate { get; set; } 
    }
}
