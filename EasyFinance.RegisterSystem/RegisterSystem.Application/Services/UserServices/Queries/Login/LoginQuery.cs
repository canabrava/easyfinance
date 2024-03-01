using ErrorOr;
using MediatR;

namespace RegisterSystem.Application.Services.UserServices.Queries.Login
{
    public record LoginQuery(
       string email,
       string password) : IRequest<ErrorOr<LoginResult>>;
}

