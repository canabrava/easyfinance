using RegisterSystem.Domain.Entities;

namespace RegisterSystem.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);

        Task<User?> GetByEmailAsync(string email);
    }
}
