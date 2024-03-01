using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using FinanceSystem.Domain.Common.Interfaces;
using FinanceSystem.Infrastructure.Persistence;
using FinanceSystem.Infrastructure;
using Microsoft.Extensions.DependencyInjection;


namespace FinanceSystem.Tests.UnitTests.Interfaces.DependencyInjection
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void AddAddInfraestructure_RegistersDependenciesCorrectly()
        {
            //Arrange
            var services = new ServiceCollection();

            //Act
            services.AddInfrastructure();

            //Assert
            var serviceProvider = services.BuildServiceProvider();

            var dateTimeProvider = serviceProvider.GetService<IDateTimeProvider>();
            var financeSystemDbContext = serviceProvider.GetService<FinanceSystemDbContext>();
            var repository = serviceProvider.GetService<IRepository<BankAccount,BankAccountId>>();

            Assert.NotNull(dateTimeProvider);
            Assert.NotNull(financeSystemDbContext);
            Assert.NotNull(repository);
        }

        [Fact]
        public void AddServices_RegistersDateTimeProviderCorrectly()
        {
            //Arrange
            var services = new ServiceCollection();

            //Act
            services.AddServices();

            //Assert
            var serviceProvider = services.BuildServiceProvider();
            var dateTimeProvider = serviceProvider.GetService<IDateTimeProvider>();

            Assert.NotNull(dateTimeProvider);
        }

        [Fact]
        public void AddDbContext_RegistersFinanceSystemDbContextCorrectly()
        {
            //Arrange
            var services = new ServiceCollection();

            //Act
            services.AddDbContext();

            //Assert
            var serviceProvider = services.BuildServiceProvider();
            var financeSystemDbContext = serviceProvider.GetService<FinanceSystemDbContext>();

            var repository = serviceProvider.GetService<IRepository<BankAccount, BankAccountId>>();

            Assert.NotNull(financeSystemDbContext);
            Assert.NotNull(repository);
        }
    }
}
