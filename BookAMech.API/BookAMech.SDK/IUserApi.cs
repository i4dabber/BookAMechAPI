using BookAMech.Contracts.V1.Requests;
using BookAMech.Contracts.V1.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookAMech.SDK
{
    public interface IUserApi
    {
        [Post("/api/v1/user/register")]
        Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body] UserRegistrationRequest registrationRequest);

        [Post("/api/v1/user/login")]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginRequest loginRequest);

        [Post("/api/v1/user/refresh")]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest tokenRequest);
    }
}
