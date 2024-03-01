using ErrorOr;
using FinanceSystem.Application.BankAccounts.Commands.DeleteBankAccount;
using FinanceSystem.Application.BankAccounts.Errors;
using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using FinanceSystem.Tests.TestUtils.BankAccount;
using FluentAssertions;
using NSubstitute;

namespace FinanceSystem.Tests.UnitTests.Application.BankAccounts
{
    public class DeleteBankAccountTests
    {
        [Fact]
        public async Task DeleteBankAccountCommandHandler_ShouldReturnSuccess_WhenBankAccountExistsAndBelongsToUser()
        {
            // Arrange
            var repository = Substitute.For<IRepository<BankAccount, BankAccountId>>();

            var handler = new DeleteBankAccountCommandHandler(repository);

            var bankAccount = BankAccountFactory.CreateBankAccount();
            var request = new DeleteBankAccountCommand(bankAccount.Id.Value, bankAccount.UserId);

            repository.GetByIdAsync(bankAccount.Id).Returns(bankAccount);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.Received().DeleteAsync(bankAccount);
            result.Value.Should().Be(Result.Success);
        }

        [Fact]
        public async Task DeleteBankAccountCommandHandler_ShouldReturnError_WhenBankAccountDoesNotExist()
        {
            // Arrange
            var repository = Substitute.For<IRepository<BankAccount, BankAccountId>>();

            var handler = new DeleteBankAccountCommandHandler(repository);

            var bankAccountId = BankAccountId.CreateUnique();
            var request = new DeleteBankAccountCommand(bankAccountId.Value, Guid.NewGuid());

            repository.GetByIdAsync(bankAccountId).Returns((BankAccount)null);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.DidNotReceive().DeleteAsync(Arg.Any<BankAccount>());
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountServiceErrors.BankAccountNotFount);
        }

        [Fact]
        public async Task DeleteBankAccountCommandHandler_ShouldReturnError_WhenBankAccountDoesNotBelongToUser()
        {
            // Arrange
            var repository = Substitute.For<IRepository<BankAccount, BankAccountId>>();

            var handler = new DeleteBankAccountCommandHandler(repository);

            var bankAccount = BankAccountFactory.CreateBankAccount();
            var request = new DeleteBankAccountCommand(bankAccount.Id.Value, Guid.NewGuid());

            repository.GetByIdAsync(bankAccount.Id).Returns(bankAccount);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            await repository.DidNotReceive().DeleteAsync(Arg.Any<BankAccount>());
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountServiceErrors.BankAccountNotBelongsToUser);
        }
    }
}
