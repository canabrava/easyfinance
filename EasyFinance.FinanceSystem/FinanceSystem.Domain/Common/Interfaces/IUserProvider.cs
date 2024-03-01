using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace FinanceSystem.Domain.Common.Interfaces
{
    public interface IUserProvider
    {
        ErrorOr<Guid> GetUserId(HttpContext httpContext);
    }
}
