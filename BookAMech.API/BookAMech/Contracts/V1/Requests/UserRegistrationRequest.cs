using System.ComponentModel.DataAnnotations;

namespace BookAMech.Contracts.V1.Requests
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }
       
        public int Phonenumber { get; set; }    
    }
}
