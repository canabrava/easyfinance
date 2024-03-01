using Microsoft.Extensions.DependencyInjection;
using RegisterSystem.Application.Common.Interfaces.Persistence;
using RegisterSystem.Application.Common.Interfaces.Services;
using RegisterSystem.Infrastructure.Persistence.Repositories;
using RegisterSystem.Infrastructure.Services;

namespace RegisterSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ICryptographyProvider, CryptographyProvider>();
            services.AddSingleton<ITokenGenerator, TokenGenerator>();


            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
