using ErrorOr;
using MediatR;

namespace RegisterSystem.Application.Services.UserServices.Commands.Register.Interfaces
{
    public interface IRegisterCommandHandler
        : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
    {
    }
}
