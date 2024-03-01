using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.Errors;
using FluentAssertions;
using FinanceSystem.Tests.TestConstants;
using FinanceSystem.Tests.TestUtils.BankAccount;
using FinanceSystem.Domain.ExpenseAggregate.ValueObjects;
using FinanceSystem.Domain.IncomeAggregate.ValueObjects;
using ErrorOr;

namespace FinanceSystem.Tests.UnitTests.Domain.BankAccontAggregate
{
    public class BankAccountTests
    {
        [Fact]
        public void Create_WithValidData_ShouldReturnBankAccount()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string accountName = Constants.BankAccount.AccountName;
            string accountNumber = Constants.BankAccount.AccountNumber;
            string bankCode = Constants.BankAccount.BankCode;
            string agency = Constants.BankAccount.Agency;
            string accountType = Constants.BankAccount.AccountType;
            decimal? balance = Constants.BankAccount.Balance;

            // Act
            var result = BankAccount.Create(userId, accountName, accountNumber, bankCode, agency, accountType, balance);

            // Assert
            var bankAccount = result.Value;

            bankAccount.Should().NotBeNull();
            bankAccount.UserId.Should().Be(userId);
            bankAccount.AccountName.Should().Be(accountName);
            bankAccount.AccountNumber.Should().Be(accountNumber);
            bankAccount.BankCode.Should().Be(bankCode);
            bankAccount.Agency.Should().Be(agency);
            bankAccount.AccountType.Should().Be(accountType);
            bankAccount.Balance.Should().Be(balance);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WithInvalidAccountName_ShouldReturnError(string accountName)
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string accountNumber = Constants.BankAccount.AccountNumber;
            string bankCode = Constants.BankAccount.BankCode;
            string agency = Constants.BankAccount.Agency;
            string accountType = Constants.BankAccount.AccountType;
            decimal? balance = Constants.BankAccount.Balance;

            // Act
            var result = BankAccount.Create(userId, accountName, accountNumber, bankCode, agency, accountType, balance);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.MustHaveAnAccountName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("123456789")]
        public void Create_WithInvalidAccountNumber_ShouldReturnError(string accountNumber)
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string accountName = Constants.BankAccount.AccountName;
            string bankCode = Constants.BankAccount.BankCode;
            string agency = Constants.BankAccount.Agency;
            string accountType = Constants.BankAccount.AccountType;
            decimal? balance = Constants.BankAccount.Balance;

            // Act
            var result = BankAccount.Create(userId, accountName, accountNumber, bankCode, agency, accountType, balance);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.MustHaveAValidAccountNumber);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("13")]
        public void Create_WithInvalidBankCode_ShouldReturnError(string bankCode)
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string accountName = Constants.BankAccount.AccountName;
            string accountNumber = Constants.BankAccount.AccountNumber;
            string agency = Constants.BankAccount.Agency;
            string accountType = Constants.BankAccount.AccountType;
            decimal? balance = Constants.BankAccount.Balance;

            // Act
            var result = BankAccount.Create(userId, accountName, accountNumber, bankCode, agency, accountType, balance);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.MustHaveAValidBankCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("123456789")]
        public void Create_WithInvalidAgency_ShouldReturnError(string agency)
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string accountName = Constants.BankAccount.AccountName;
            string accountNumber = Constants.BankAccount.AccountNumber;
            string bankCode = Constants.BankAccount.BankCode;
            string accountType = Constants.BankAccount.AccountType;
            decimal? balance = Constants.BankAccount.Balance;

            // Act
            var result = BankAccount.Create(userId, accountName, accountNumber, bankCode, agency, accountType, balance);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.MustHaveAValidAgency);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WithInvalidAccountType_ShouldReturnError(string accountType)
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string accountName = Constants.BankAccount.AccountName;
            string accountNumber = Constants.BankAccount.AccountNumber;
            string bankCode = Constants.BankAccount.BankCode;
            string agency = Constants.BankAccount.Agency;
            decimal? balance = Constants.BankAccount.Balance;

            // Act
            var result = BankAccount.Create(userId, accountName, accountNumber, bankCode, agency, accountType, balance);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.MustHaveAnAccountType);
        }

        [Fact]
        public void Create_WithNegativeBalance_ShouldReturnError()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string accountName = Constants.BankAccount.AccountName;
            string accountNumber = Constants.BankAccount.AccountNumber;
            string bankCode = Constants.BankAccount.BankCode;
            string agency = Constants.BankAccount.Agency;
            string accountType = Constants.BankAccount.AccountType;
            decimal? balance = -Constants.BankAccount.Balance;

            // Act
            var result = BankAccount.Create(userId, accountName, accountNumber, bankCode, agency, accountType, balance);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.BalanceMustBeZeroOrPositive);
        }

        [Fact]
        public void UpdateBankAccountDetails_WithValidData_ShouldUpdateBankAccount()
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount();

            string accountName = Constants.BankAccount.AccountName;
            string agency = Constants.BankAccount.Agency;
            string accountType  = Constants.BankAccount.AccountType;

            // Act
            var result = bankAccount.UpdateBankAccountDetails(accountName, agency, accountType);

            // Assert
            result.Value.Should().BeEquivalentTo(Result.Success);
            bankAccount.AccountName.Should().Be(accountName);
            bankAccount.Agency.Should().Be(agency);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("123456789")]
        public void UpdateBankAccountDetails_WithInvalidAgency_ShouldReturnError(string agency)
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount();

            string accountName = Constants.BankAccount.AccountName;
            string accountType = Constants.BankAccount.AccountType;

            // Act
            var result = bankAccount.UpdateBankAccountDetails(accountName, agency, accountType);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.MustHaveAValidAgency);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UpdateBankAccountDetails_WithInvalidAccountType_ShouldReturnError(string accountType)
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount();

            string accountName = Constants.BankAccount.AccountName;
            string agency = Constants.BankAccount.Agency;

            // Act
            var result = bankAccount.UpdateBankAccountDetails(accountName, agency, accountType);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.MustHaveAnAccountType);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UpdateBankAccountDetails_WithInvalidAccountName_ShouldReturnError(string accountName)
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount();

            string agency = Constants.BankAccount.Agency;
            string accountType = Constants.BankAccount.AccountType;

            // Act
            var result = bankAccount.UpdateBankAccountDetails(accountName, agency, accountType);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.MustHaveAnAccountName);
        }

        [Fact]
        public void AddExpenseTransaction_WithValidData_ShouldAddFinancialTransaction()
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount(balance: 1000);

            var expenseId = ExpenseId.CreateUnique();
            var financialTransaction = FinancialTransactionFactory.CreateFinancialTransaction(
                amount: 100,
                expenseId: expenseId);

            // Act
            bankAccount.AddTransaction(financialTransaction);

            // Assert
            bankAccount.FinancialTransactions.Should().Contain(financialTransaction);
            bankAccount.Balance.Should().Be(900);
        }

        [Fact]
        public void AddIncomeTransaction_WithValidData_ShouldAddFinancialTransaction()
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount(balance: 1000);

            var incomeId = IncomeId.CreateUnique();
            var financialTransaction = FinancialTransactionFactory.CreateFinancialTransaction(
                               amount: 100,
                                              incomeId: incomeId);

            // Act
            bankAccount.AddTransaction(financialTransaction);

            // Assert
            bankAccount.FinancialTransactions.Should().Contain(financialTransaction);
            bankAccount.Balance.Should().Be(1100);
        }

        [Fact]
        public void AddExpenseTransaction_WithoutEnoughBalance_ShouldReturnError()
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount(balance: 100);

            var expenseId = ExpenseId.CreateUnique();
            var financialTransaction = FinancialTransactionFactory.CreateFinancialTransaction(
                amount: 1000,
                expenseId: expenseId);

            // Act
            var result = bankAccount.AddTransaction(financialTransaction);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.NotEnoughBalance);
            bankAccount.FinancialTransactions.Should().NotContain(financialTransaction);
        }

        [Fact]
        public void RemoveExpenseTransaction_WithValidData_ShouldRemoveFinancialTransaction()
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount(balance: 1000);

            var expenseId = ExpenseId.CreateUnique();
            var financialTransaction = FinancialTransactionFactory.CreateFinancialTransaction(
                amount: 100,
                expenseId: expenseId);

            bankAccount.AddTransaction(financialTransaction);

            // Act
            bankAccount.RemoveTransaction(financialTransaction);

            // Assert
            bankAccount.FinancialTransactions.Should().NotContain(financialTransaction);
            bankAccount.Balance.Should().Be(1000);
        }

        [Fact]
        public void RemoveIncomeTransaction_WithValidData_ShouldRemoveFinancialTransaction()
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount(balance: 1000);

            var incomeId = IncomeId.CreateUnique();
            var financialTransaction = FinancialTransactionFactory.CreateFinancialTransaction(
                               amount: 100,
                                              incomeId: incomeId);

            bankAccount.AddTransaction(financialTransaction);

            // Act
            bankAccount.RemoveTransaction(financialTransaction);

            // Assert
            bankAccount.FinancialTransactions.Should().NotContain(financialTransaction);
            bankAccount.Balance.Should().Be(1000);
        }

        [Fact]
        public void RemoveIncomeTransaction_WithoutEnoughBalance_ShouldReturnError()
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount(balance: 100);

            var incomeId = IncomeId.CreateUnique();
            var financialTransaction = FinancialTransactionFactory.CreateFinancialTransaction(
                amount: 1000,
                incomeId: incomeId);

            var expenseId = ExpenseId.CreateUnique();
            var financialExpenseTransaction = FinancialTransactionFactory.CreateFinancialTransaction(
                amount: 1000,
                expenseId: expenseId);


            bankAccount.AddTransaction(financialTransaction);
            bankAccount.AddTransaction(financialExpenseTransaction);


            // Act
            var result = bankAccount.RemoveTransaction(financialTransaction);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.NotEnoughBalance);
            bankAccount.FinancialTransactions.Should().Contain(financialTransaction);
        }

        [Fact]
        public void RemoveTransaction_WithNotFoundTransaction_ShouldReturnError()
        {
            // Arrange
            var bankAccount = BankAccountFactory.CreateBankAccount(balance: 1000);

            var expenseId = ExpenseId.CreateUnique();
            var financialTransaction = FinancialTransactionFactory.CreateFinancialTransaction(
                               amount: 100,
                                              expenseId: expenseId);

            // Act
            var result = bankAccount.RemoveTransaction(financialTransaction);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(BankAccountErrors.TransactionNotFound);
        }
    }
}
