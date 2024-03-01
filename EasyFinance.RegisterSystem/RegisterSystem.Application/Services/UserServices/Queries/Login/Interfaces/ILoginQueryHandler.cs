using ErrorOr;
using MediatR;

namespace RegisterSystem.Application.Services.UserServices.Queries.Login.Interfaces
{
    public interface ILoginQueryHandler
        : IRequestHandler<LoginQuery, ErrorOr<LoginResult>>
    {
    }
}
