using FinanceSystem.Contracts.BankAccounts.Validators;
using FinanceSystem.Contracts.BankAccounts;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace FinanceSystem.Contracts
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContracts(this IServiceCollection services)
        {
            services.AddTransient<IValidator<BankAccountRequest>, BankAccountRequestValidator>();

            return services;
        }
    }
}
