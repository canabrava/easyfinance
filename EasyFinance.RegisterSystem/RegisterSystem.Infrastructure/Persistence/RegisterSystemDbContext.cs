using Microsoft.EntityFrameworkCore;
using RegisterSystem.Domain.Entities;
using RegisterSystem.Infrastructure.Configurations;

namespace RegisterSystem.Infrastructure.Persistence
{
    public class RegisterSystemDbContext : DbContext
    {
        public RegisterSystemDbContext(DbContextOptions<RegisterSystemDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfigurations());
        }
    }
}
