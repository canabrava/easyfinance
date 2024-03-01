using ErrorOr;
using FinanceSystem.Application.BankAccounts.Commands.UpdateBankAccount;
using FinanceSystem.Application.BankAccounts.Errors;
using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using FinanceSystem.Tests.TestUtils.BankAccount;
using FluentAssertions;
using NSubstitute;

namespace FinanceSystem.Tests.UnitTests.Application.BankAccounts
{
    public class UpdateBankAccountTests
    {
        [Fact]
        public async Task UpdateBankAccountCommandHandler_ShouldReturnSuccess_WhenBankAccountExistsAndBelongsToUser()
        {
            // Arrange
            var repository = Substitute.For<IRepository<BankAccount, BankAccountId>>();

            var handler = new UpdateBankAccountCommandHandler(repository);

            var bankAccount = BankAccountFactory.CreateBankAccount();
            var request = new UpdateBankAccountCommand(
                bankAccount.UserId,
                bankAccount.Id.Value,
                "New Account Name",
                "New Agency",
                "New Account Type");

            repository.GetByIdAsync(Arg.Any<BankAccountId>()).Returns(bankAccount);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received().GetByIdAsync(Arg.Any<BankAccountId>());
            await repository.Received().UpdateAsync(Arg.Any<BankAccount>());
            result.Value.Should().BeEquivalentTo(Result.Success);
        }

        [Fact]
        public async Task UpdateBankAccountCommandHandler_ShouldReturnError_WhenBankAccountDoesNotExist()
        {
            // Arrange
            var repository = Substitute.For<IRepository<BankAccount, BankAccountId>>();

            var handler = new UpdateBankAccountCommandHandler(repository);

            var request = new UpdateBankAccountCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "New Account Name",
                "New Agency",
                "New Account Type");

            repository.GetByIdAsync(Arg.Any<BankAccountId>()).Returns((BankAccount)null);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received().GetByIdAsync(Arg.Any<BankAccountId>());
            await repository.DidNotReceive().UpdateAsync(Arg.Any<BankAccount>());
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountServiceErrors.BankAccountNotFount);
        }

        [Fact]
        public async Task UpdateBankAccountCommandHandler_ShouldReturnError_WhenBankAccountDoesNotBelongToUser()
        {
            // Arrange
            var repository = Substitute.For<IRepository<BankAccount, BankAccountId>>();

            var handler = new UpdateBankAccountCommandHandler(repository);

            var bankAccount = BankAccountFactory.CreateBankAccount();
            var request = new UpdateBankAccountCommand(
                Guid.NewGuid(),
                bankAccount.Id.Value,
                "New Account Name",
                "New Agency",
                "New Account Type");

            repository.GetByIdAsync(Arg.Any<BankAccountId>()).Returns(bankAccount);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received().GetByIdAsync(Arg.Any<BankAccountId>());
            await repository.DidNotReceive().UpdateAsync(Arg.Any<BankAccount>());
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountServiceErrors.BankAccountNotBelongsToUser);
        }
    }
}
