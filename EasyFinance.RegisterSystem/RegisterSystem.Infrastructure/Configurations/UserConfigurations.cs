using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegisterSystem.Domain.Entities;

namespace RegisterSystem.Infrastructure.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUsersTable(builder);
        }

        private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(200); 

            builder.Property(u => u.Salt)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.RegisterDate)
                .IsRequired()
                .HasColumnType("timestamp");
        }
    }
}
