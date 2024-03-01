using FinanceSystem.Application.BankAccounts.Queries.GetUserBankAccounts;
using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using FinanceSystem.Tests.TestConstants;
using FinanceSystem.Tests.TestUtils.BankAccount;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace FinanceSystem.Tests.UnitTests.Application.BankAccounts
{
    public class GetUserBankAccountsTests
    {
        [Fact]
        public async Task GetUserBankAccountsQueryHandler_ShouldReturnUserBankAccounts()
        {
            // Arrange
            var repository = Substitute.For<IRepository<BankAccount, BankAccountId>>();
            var handler = new GetUserBankAccountsQueryHandler(repository);

            var userId = Guid.NewGuid();
            var bankAccounts = new List<BankAccount>
            {
                BankAccountFactory.CreateBankAccount(userId: userId),
                BankAccountFactory.CreateBankAccount(userId: userId),
                BankAccountFactory.CreateBankAccount(userId: userId)
            };

            repository.FindAsync(Arg.Any<Expression<Func<BankAccount, bool>>>())
                .Returns(bankAccounts);

            var query = new GetUserBankAccountsQuery(userId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);

            await repository.Received().FindAsync(Arg.Any<Expression<Func<BankAccount, bool>>>());

            foreach (var bankAccount in result)
            {
                bankAccount.AccountName.Should().Be(Constants.BankAccount.AccountName);
                bankAccount.AccountNumber.Should().Be(Constants.BankAccount.AccountNumber);
                bankAccount.BankCode.Should().Be(Constants.BankAccount.BankCode);
                bankAccount.Agency.Should().Be(Constants.BankAccount.Agency);
                bankAccount.AccountType.Should().Be(Constants.BankAccount.AccountType);
                bankAccount.Balance.Should().Be(Constants.BankAccount.Balance);
            }
        }
    }
}
