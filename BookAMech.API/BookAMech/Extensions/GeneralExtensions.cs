using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BookAMech.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User == null) //Check for userId
            {
                return string.Empty;
            }

            return httpContext.User.Claims.Single(x => x.Type == "id").Value; 
            //Single() because there should be only one id
            //Returns userId from Token
        }
    }
}
