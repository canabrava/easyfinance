using ErrorOr;
using FinanceSystem.Application.BankAccounts.Commands.AddBankAccount;
using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.Errors;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using FinanceSystem.Tests.TestUtils.BankAccount;
using FinanceSystem.Tests.TestConstants;
using FluentAssertions;
using NSubstitute;

namespace FinanceSystem.Tests.UnitTests.Application.BankAccounts
{
    public class AddBankAccountTests
    {
        [Fact]
        public async Task AddBankAccountCommandHandler_ShouldReturnSuccess_WhenBankAccountIsValid()
        {
            // Arrange
            var repository = Substitute.For<IRepository<BankAccount, BankAccountId>>();

            var handler = new AddBankAccountCommandHandler(repository);

            var bankAccount = BankAccountFactory.CreateBankAccount();

            var request = new AddBankAccountCommand(
                bankAccount.UserId,
                bankAccount.AccountName,
                bankAccount.AccountNumber,
                bankAccount.BankCode,
                bankAccount.Agency,
                bankAccount.AccountType,
                bankAccount.Balance);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received().AddAsync(Arg.Any<BankAccount>());
            result.Value.GetType().Should().Be<Guid>();
        }

        [Fact]
        public async Task AddBankAccountCommandHandler_ShouldReturnError_WhenBankAccountIsInvalid()
        {
            // Arrange
            var repository = Substitute.For<IRepository<BankAccount, BankAccountId>>();

            var handler = new AddBankAccountCommandHandler(repository);

            Guid userId = Guid.NewGuid();
            string accountName = Constants.BankAccount.AccountName;
            string accountNumber = Constants.BankAccount.AccountNumber;
            string bankCode = Constants.BankAccount.BankCode;
            string agency = Constants.BankAccount.Agency;
            string accountType = Constants.BankAccount.AccountType;
            decimal? balance = -Constants.BankAccount.Balance;

            var request = new AddBankAccountCommand(
                userId,
                accountName,
                accountNumber,
                bankCode,
                agency,
                accountType,
                balance);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.DidNotReceive().AddAsync(Arg.Any<BankAccount>());
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.BalanceMustBeZeroOrPositive);
        }
    }
}
