using Xunit;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FinanceSystem.Contracts.BankAccounts;
using FinanceSystem.Contracts.BankAccounts.Validators;
using FinanceSystem.Contracts;


namespace FinanceSystem.Tests.UnitTests.Contracts.DependencyInjection
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void AddContracts_RegistersDependenciesCorrectly()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddContracts();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var validator = serviceProvider.GetService<IValidator<BankAccountRequest>>();
            Assert.NotNull(validator);
            Assert.IsType<BankAccountRequestValidator>(validator);
        }
    }
}
