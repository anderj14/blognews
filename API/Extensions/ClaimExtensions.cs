
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user?.FindFirstValue(ClaimTypes.Email);
        }
    }
}