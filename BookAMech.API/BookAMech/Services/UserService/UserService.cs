using BookAMech.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System;
using BookAMech.Settings;

namespace BookAMech.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly JwtSettings _jwtSettings;

        public UserService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager; 
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password, int phone)
        {
           var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exists." }
                };

            //Create new user
            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email,
                PhoneNumber = phone.ToString()
            };

            var createUser = await _userManager.CreateAsync(newUser, password);

            if (!createUser.Succeeded)
                return new AuthenticationResult
                {
                    Errors = createUser.Errors.Select(x => x.Description)
                };       
            
            return GetAuthenticationForUser(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
                return new AuthenticationResult
                {
                    Errors = new[] { "Password does not match" }
                };

            return GetAuthenticationForUser(user);
        }

        private AuthenticationResult GetAuthenticationForUser(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] //Claims are a bunch of properties in our token that tells us about the specific user
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
