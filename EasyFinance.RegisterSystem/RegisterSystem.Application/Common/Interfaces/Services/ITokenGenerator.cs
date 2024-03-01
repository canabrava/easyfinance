using RegisterSystem.Domain.Entities;

namespace RegisterSystem.Application.Common.Interfaces.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
