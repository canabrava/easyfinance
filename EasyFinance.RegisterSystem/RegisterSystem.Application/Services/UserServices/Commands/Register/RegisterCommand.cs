using ErrorOr;
using MediatR;

namespace RegisterSystem.Application.Services.UserServices.Commands.Register
{
    public record RegisterCommand(
        string name,
        string email,
        string password) : IRequest<ErrorOr<RegisterResult>>;
}
