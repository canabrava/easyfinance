using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.Common.Interfaces;
using FinanceSystem.Infrastructure.Persistence;
using FinanceSystem.Infrastructure.Persistence.Repositories.Common;
using FinanceSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinanceSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddDbContext()
                .AddAuthentication()
                .AddAuthorization()
                .AddServices();

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            var relativePath = @"..\..\Database\finance_system.db";

            var absolutePath = Path.GetFullPath(relativePath);

            services.AddDbContext<FinanceSystemDbContext>(options =>
                options.UseSqlite($"Data Source={absolutePath}"));

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("CHAVE_SUPER_SECRETA_CHAVE_SUPER_SECRETA")),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                        };
                    });

            return services;
        }
    }
}
