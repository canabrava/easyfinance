using FluentAssertions;
using FinanceSystem.Domain.ExpenseAggregate;
using FinanceSystem.Domain.ExpenseAggregate.Errors;
using FinanceSystem.Tests.TestUtils.Expense;
using ErrorOr;

namespace FinanceSystem.Tests.UnitTests.Domain.ExpenseAggregate
{
    public class ExpenseTests
    {
        [Fact]
        public void Create_ShouldReturnExpense_WhenValidParameters()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var amount = 100.50m;
            var description = "Expense description";
            var creationDate = DateTime.Now;
            var duePaymentDate = DateTime.Now.AddDays(7);
            var paymentDate = DateTime.Now.AddDays(5);
            var paid = true;
            var recurrent = true;

            // Act
            var result = Expense.Create(userId, amount, description, creationDate, duePaymentDate, paymentDate, paid, recurrent);

            // Assert
            var expense = result.Value;

            expense.Should().BeOfType<Expense>();
            expense.UserId.Should().Be(userId);
            expense.Amount.Should().Be(amount);
            expense.Description.Should().Be(description);
            expense.CreationDate.Should().Be(creationDate);
            expense.DuePaymentDate.Should().Be(duePaymentDate);
            expense.PaymentDate.Should().Be(paymentDate);
            expense.Paid.Should().Be(paid);
            expense.Recurrent.Should().Be(recurrent);
            expense.Payments.Should().BeEmpty();
        }

        [Fact]
        public void Create_ShouldReturnError_WhenAmountIsZero()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var amount = 0m;
            var description = "Expense description";
            var creationDate = DateTime.Now;
            var duePaymentDate = DateTime.Now.AddDays(7);
            var paymentDate = DateTime.Now.AddDays(5);
            var paid = true;
            var recurrent = true;

            // Act
            var result = Expense.Create(userId, amount, description, creationDate, duePaymentDate, paymentDate, paid, recurrent);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().BeEquivalentTo(ExpenseErrors.MustBeAPositiveValue);
        }

        [Fact]
        public void AddPayment_ShouldReturnSuccess_WhenValidPayment()
        {
            // Arrange
            var payment = PaymentFactory.CreatePayment();
            var expense = ExpenseFactory.CreateExpense();

            // Act
            var result = expense.AddPayment(payment);

            // Assert
            result.Value.Should().BeEquivalentTo(Result.Success);
            expense.Payments.Should().Contain(payment);
        }

        [Fact]
        public void AddPayment_ShouldReturnError_WhenExpenseIsAlreadyPaid()
        {
            // Arrange
            var payment = PaymentFactory.CreatePayment(amount: 100);
            var expense = ExpenseFactory.CreateExpense(paid: true);

            // Act
            var result = expense.AddPayment(payment);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().BeEquivalentTo(ExpenseErrors.ExpenseAlreadyPaid);
            expense.Payments.Should().NotContain(payment);
        }

        [Fact]
        public void AddPayment_ShouldReturnError_WhenPaymentAmountExceedsExpenseAmount()
        {
            // Arrange
            var payment = PaymentFactory.CreatePayment(amount: 100);
            var expense = ExpenseFactory.CreateExpense(amount: 50);

            // Act
            var result = expense.AddPayment(payment);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().BeEquivalentTo(ExpenseErrors.PaymentAmountExceedsExpenseAmount);
            expense.Payments.Should().NotContain(payment);
        }

        [Fact]
        public void RemovePayment_ShouldReturnSuccess_WhenValidPayment()
        {
            // Arrange
            var payment = PaymentFactory.CreatePayment();
            var expense = ExpenseFactory.CreateExpense();
            expense.AddPayment(payment);

            // Act
            var result = expense.RemovePayment(payment);

            // Assert
            result.Value.Should().BeEquivalentTo(Result.Success);
            expense.Payments.Should().NotContain(payment);
        }

        [Fact]
        public void RemovePayment_ShouldReturnError_WhenPaymentNotFound()
        {
            // Arrange
            var payment = PaymentFactory.CreatePayment();
            var expense = ExpenseFactory.CreateExpense();

            // Act
            var result = expense.RemovePayment(payment);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().BeEquivalentTo(ExpenseErrors.PaymentNotFound);
            expense.Payments.Should().NotContain(payment);
        }

        [Fact]
        public void UpdateExpenseDetails_ShouldReturnSuccess_WhenValidParameters()
        {
            // Arrange
            var expense = ExpenseFactory.CreateExpense();
            var amount = 200m;
            var description = "Updated expense description";
            var duePaymentDate = DateTime.Now.AddDays(14);
            var recurrent = true;

            // Act
            var result = expense.UpdateExpenseDetails(amount, description, duePaymentDate, recurrent);

            // Assert
            result.Value.Should().BeEquivalentTo(Result.Success);
            expense.Amount.Should().Be(amount);
            expense.Description.Should().Be(description);
            expense.DuePaymentDate.Should().Be(duePaymentDate);
            expense.Recurrent.Should().Be(recurrent);
        }

        [Fact]
        public void UpdateExpenseDetails_ShouldReturnError_WhenAmountIsZero()
        {
            // Arrange
            var expense = ExpenseFactory.CreateExpense();
            var amount = 0m;
            var description = "Updated expense description";
            var duePaymentDate = DateTime.Now.AddDays(14);
            var recurrent = true;

            // Act
            var result = expense.UpdateExpenseDetails(amount, description, duePaymentDate, recurrent);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().BeEquivalentTo(ExpenseErrors.MustBeAPositiveValue);
        }
    }
}