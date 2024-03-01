using ErrorOr;
using FinanceSystem.Domain.IncomeAggregate;
using FinanceSystem.Domain.IncomeAggregate.Errors;
using FinanceSystem.Tests.TestUtils.Income;
using FluentAssertions;

namespace FinanceSystem.Tests.UnitTests.Domain.IncomeAggregate
{
    public class IncomeTests
    {
        [Fact]
        public void Create_ShouldReturnIncome_WhenValidParametersProvided()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var value = 1000m;
            var description = "Salary";
            var creationDate = DateTime.Now;
            var expectedReceiptDate = DateTime.Now.AddDays(30);
            var receiptDate = DateTime.Now.AddDays(30);
            var received = true;
            var recurrent = true;

            // Act
            var result = Income.Create(
                userId,
                value,
                description,
                creationDate,
                expectedReceiptDate,
                receiptDate,
                received,
                recurrent);

            // Assert
            result.Value.Should().BeOfType<Income>();
            var income = result.Value;
            income.UserId.Should().Be(userId);
            income.Amount.Should().Be(value);
            income.Description.Should().Be(description);
            income.CreationDate.Should().Be(creationDate);
            income.ExpectedReceiptDate.Should().Be(expectedReceiptDate);
            income.ReceiptDate.Should().Be(receiptDate);
            income.Received.Should().Be(received);
            income.Recurrent.Should().Be(recurrent);
        }

        [Fact]
        public void Create_ShouldReturnError_WhenValueIsNegative()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var value = -1000m;
            var description = "Salary";
            var creationDate = DateTime.Now;
            var expectedReceiptDate = DateTime.Now.AddDays(30);
            var receiptDate = DateTime.Now.AddDays(30);
            var received = true;
            var recurrent = true;

            // Act
            var result = Income.Create(
                userId,
                value,
                description,
                creationDate,
                expectedReceiptDate,
                receiptDate,
                received,
                recurrent);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().BeEquivalentTo(IncomeErrors.MustBeAPositiveValue);
        }

        [Fact]
        public void ReceiveIncome_ShouldSetReceivedAndReceiptDate_WhenIncomeNotReceived()
        {
            // Arrange
            var income = IncomeFactory.CreateIncome();
            var receiptDate = DateTime.Now;

            // Act
            var result = income.ReceiveIncome(receiptDate);

            // Assert
            result.Value.Should().BeEquivalentTo(Result.Success);
            income.Received.Should().BeTrue();
            income.ReceiptDate.Should().Be(receiptDate);
        }

        [Fact]
        public void ReceiveIncome_ShouldReturnError_WhenIncomeAlreadyReceived()
        {
            // Arrange
            var receiptDate = DateTime.Now;
            var income = IncomeFactory.CreateIncome(
                receiptDate: receiptDate,
                received: true);

            // Act
            var result = income.ReceiveIncome(receiptDate);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.First().Should().BeEquivalentTo(IncomeErrors.IncomeAlreadyReceived);
            income.Received.Should().BeTrue();
            income.ReceiptDate.Should().Be(receiptDate);
        }

        [Fact]
        public void Update_ShouldUpdateIncome_WhenValidIncomeProvided()
        {
            // Arrange
            var income = IncomeFactory.CreateIncome();

            var newIncome = IncomeFactory.CreateIncome(
                value: 2000m,
                description: "Bonus",
                expectedReceiptDate: DateTime.Now.AddDays(14),
                recurrent: true);

            // Act
            var result = income.Update(newIncome);

            // Assert
            result.Value.Should().BeEquivalentTo(Result.Success);
            income.Amount.Should().Be(newIncome.Amount);
            income.Description.Should().Be(newIncome.Description);
            income.ExpectedReceiptDate.Should().Be(newIncome.ExpectedReceiptDate);
            income.Recurrent.Should().Be(newIncome.Recurrent);
        }
    }
}