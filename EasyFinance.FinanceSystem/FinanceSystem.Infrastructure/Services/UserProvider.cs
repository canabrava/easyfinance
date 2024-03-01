using ErrorOr;
using FinanceSystem.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FinanceSystem.Infrastructure.Services
{
    public class UserProvider : IUserProvider
    {
        public ErrorOr<Guid> GetUserId(HttpContext httpContext)
        {
            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.UserData)?.Value;

            Guid userId;

            if (string.IsNullOrEmpty(userIdClaim) ||
                !Guid.TryParse(userIdClaim, out userId) )
            {
                return Error.Unauthorized();
            }

            return userId;
        }
    }
}
