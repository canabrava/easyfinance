using Microsoft.EntityFrameworkCore;
using RegisterSystem.Application.Common.Interfaces.Persistence;
using RegisterSystem.Domain.Entities;
using System;

namespace RegisterSystem.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RegisterSystemDbContext _context;

        public UserRepository(RegisterSystemDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
