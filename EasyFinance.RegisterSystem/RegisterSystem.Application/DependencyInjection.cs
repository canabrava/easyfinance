using ErrorOr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RegisterSystem.Application.Common.Interfaces.Services;
using RegisterSystem.Application.Services.UserServices.Commands.Register;
using RegisterSystem.Application.Services.UserServices.Commands.Register.Interfaces;
using RegisterSystem.Application.Services.UserServices.Queries.Login;
using RegisterSystem.Application.Services.UserServices.Queries.Login.Interfaces;

namespace RegisterSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
           
            services.AddScoped<IRegisterCommandHandler, RegisterCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>, RegisterCommandLoggingDecorator>(
                serviceProvider => new RegisterCommandLoggingDecorator(
                    serviceProvider.GetRequiredService<IRegisterCommandHandler>(), 
                    serviceProvider.GetRequiredService<ILogger<IRegisterCommandHandler>>(),
                    serviceProvider.GetRequiredService<IDateTimeProvider>()));

            services.AddScoped<ILoginQueryHandler, LoginQueryHandler>();
            services.AddScoped<IRequestHandler<LoginQuery, ErrorOr<LoginResult>>, LoginQueryLoggingDecorator>(
                serviceProvider => new LoginQueryLoggingDecorator(
                    serviceProvider.GetRequiredService<ILoginQueryHandler>(),
                    serviceProvider.GetRequiredService<ILogger<ILoginQueryHandler>>(),
                    serviceProvider.GetRequiredService<IDateTimeProvider>()));

            return services;
        }
    }
}
