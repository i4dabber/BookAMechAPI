using BookAMech.Domain;
using System.Threading.Tasks;

namespace BookAMech.Services
{
    public interface IUserService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password, int phone);
        Task<AuthenticationResult> LoginAsync(string email, string password);

        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

    }
}
