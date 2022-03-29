using BookAMech.Contracts.V1;
using BookAMech.Contracts.V1.Requests;
using BookAMech.Contracts.V1.Responses;
using BookAMech.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookAMech.Controllers.V1
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(ApiRoutes.Users.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage))
                });
            }

            var authResponse = await _userService.RegisterAsync(request.Email, request.Password, request.Phonenumber);

            if(!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }

        [HttpPost(ApiRoutes.Users.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _userService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token
            });
        }
    }
}
