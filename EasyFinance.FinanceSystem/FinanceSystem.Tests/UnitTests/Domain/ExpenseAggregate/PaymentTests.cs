using FinanceSystem.Domain.ExpenseAggregate.Entities;
using FinanceSystem.Domain.ExpenseAggregate.Errors;
using FluentAssertions;

namespace FinanceSystem.Tests.UnitTests.Domain.ExpenseAggregate
{
    public class PaymentTests
    {
        [Fact]
        public void CreateUnique_ShouldReturnPayment_WhenValidInput()
        {
            // Arrange
            var date = DateTime.Now;
            var amount = 100.50m;
            var description = "Payment description";

            // Act
            var result = Payment.Create(date, amount, description);

            // Assert
            result.Value.Should().BeOfType<Payment>();
            result.Value.PaymentDate.Should().Be(date);
            result.Value.Amount.Should().Be(amount);
            result.Value.Description.Should().Be(description);
        }

        [Fact]
        public void CreateUnique_ShouldReturnError_WhenAmountIsZero()
        {
            // Arrange
            var date = DateTime.Now;
            var amount = 0m;
            var description = "Payment description";

            // Act
            var result = Payment.Create(date, amount, description);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().BeEquivalentTo(PaymentErrors.MustBeAPositiveValue);
        }

        [Fact]
        public void CreateUnique_ShouldReturnError_WhenAmountIsNegative()
        {
            // Arrange
            var date = DateTime.Now;
            var amount = -100m;
            var description = "Payment description";

            // Act
            var result = Payment.Create(date, amount, description);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().BeEquivalentTo(PaymentErrors.MustBeAPositiveValue);
        }
    }
}