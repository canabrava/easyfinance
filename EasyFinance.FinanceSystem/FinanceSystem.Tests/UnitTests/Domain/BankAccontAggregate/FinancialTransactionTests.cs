using FinanceSystem.Domain.BankAccountAggregate.Entities;
using FinanceSystem.Domain.BankAccountAggregate.Errors;
using FinanceSystem.Domain.ExpenseAggregate.ValueObjects;
using FinanceSystem.Domain.IncomeAggregate.ValueObjects;
using FluentAssertions;

namespace FinanceSystem.Tests.UnitTests.Domain.BankAccontAggregate
{
    public class FinancialTransactionTests
    {
        [Fact]
        public void Create_ShouldReturnFinancialTransaction_WhenValidParameters_WithExpense()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var amount = 100;
            var expenseId = ExpenseId.CreateUnique();

            // Act
            var result = FinancialTransaction.Create(creationDate, amount, expenseId: expenseId);

            // Assert
            var financialTransaction = result.Value;
            financialTransaction.Should().BeOfType<FinancialTransaction>();
            financialTransaction.CreationDate.Should().Be(creationDate);
            financialTransaction.Amount.Should().Be(amount);
            financialTransaction.ExpenseId.Should().Be(expenseId);
            financialTransaction.IncomeId.Should().BeNull();
        }

        [Fact]
        public void Create_ShouldReturnFinancialTransaction_WhenValidParameters_WithIncome()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var amount = 100;
            var incomeId = IncomeId.CreateUnique();

            // Act
            var result = FinancialTransaction.Create(creationDate, amount, incomeId: incomeId);

            // Assert
            var financialTransaction = result.Value;
            financialTransaction.Should().BeOfType<FinancialTransaction>();
            financialTransaction.CreationDate.Should().Be(creationDate);
            financialTransaction.Amount.Should().Be(amount);
            financialTransaction.IncomeId.Should().Be(incomeId);
            financialTransaction.ExpenseId.Should().BeNull();
        }

        [Fact]
        public void Create_ShouldReturnMustBeAPositiveValueError_WhenAmountIsZero()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var amount = 0;
            var expenseId = ExpenseId.CreateUnique();

            // Act
            var result = FinancialTransaction.Create(creationDate, amount, expenseId);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(FinancialTransactionErrors.MustBeAPositiveValue);
        }

        [Fact]
        public void Create_ShouldReturnMustHaveAnExpenseOrIncomeError_WhenBothExpenseIdAndIncomeIdAreNull()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var amount = 100;

            // Act
            var result = FinancialTransaction.Create(creationDate, amount);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(FinancialTransactionErrors.MustHaveAnExpenseOrIncome);
        }

        [Fact]
        public void Create_ShouldReturnCannotHaveBothExpenseAndIncomeError_WhenBothExpenseIdAndIncomeIdAreNotNull()
        {
            // Arrange
            var creationDate = DateTime.Now;
            var amount = 100;
            var expenseId = ExpenseId.CreateUnique();
            var incomeId = IncomeId.CreateUnique();

            // Act
            var result = FinancialTransaction.Create(creationDate, amount, expenseId, incomeId);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().Be(FinancialTransactionErrors.CannotHaveBothExpenseAndIncome);
        }

        [Fact]
        public void IsExpense_ShouldReturnTrue_WhenExpenseIdIsNotNull()
        {
            // Arrange
            var financialTransaction = TestUtils.BankAccount.FinancialTransactionFactory
                .CreateFinancialTransaction(expenseId: ExpenseId.CreateUnique());

            // Act
            var isExpense = financialTransaction.IsExpense();

            // Assert
            isExpense.Should().BeTrue();
        }

        [Fact]
        public void IsExpense_ShouldReturnFalse_WhenExpenseIdIsNull()
        {
            // Arrange
            var financialTransaction = TestUtils.BankAccount.FinancialTransactionFactory
                .CreateFinancialTransaction(incomeId: IncomeId.CreateUnique());

            // Act
            var isExpense = financialTransaction.IsExpense();

            // Assert
            isExpense.Should().BeFalse();
        }

        [Fact]
        public void IsIncome_ShouldReturnTrue_WhenIncomeIdIsNotNull()
        {
            // Arrange
            var financialTransaction = TestUtils.BankAccount.FinancialTransactionFactory
                .CreateFinancialTransaction(incomeId: IncomeId.CreateUnique());

            // Act
            var isExpense = financialTransaction.IsIncome();

            // Assert
            isExpense.Should().BeTrue();
        }

        [Fact]
        public void IsIncome_ShouldReturnFalse_WhenIncomeIdIsNull()
        {
            // Arrange
            var financialTransaction = TestUtils.BankAccount.FinancialTransactionFactory
                .CreateFinancialTransaction(expenseId: ExpenseId.CreateUnique());

            // Act
            var isExpense = financialTransaction.IsIncome();

            // Assert
            isExpense.Should().BeFalse();
        }
    }
}
