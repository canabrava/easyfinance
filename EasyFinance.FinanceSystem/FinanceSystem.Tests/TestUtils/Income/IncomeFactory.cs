using FinanceSystem.Tests.TestConstants;

namespace FinanceSystem.Tests.TestUtils.Income
{
    public static class IncomeFactory
    {
        public static Domain.IncomeAggregate.Income CreateIncome(
            Guid? userId = null,
            decimal? value = null,
            string? description = null,
            DateTime? creationDate = null,
            DateTime? expectedReceiptDate = null,
            DateTime? receiptDate = null,
            bool? received = null,
            bool? recurrent = null)
        {
            return Domain.IncomeAggregate.Income.Create(
                userId ?? Constants.User.GetUserId(),
                value ?? Constants.Income.Value,
                description ?? Constants.Income.Description,
                creationDate ?? DateTime.Now,
                expectedReceiptDate ?? DateTime.Now.AddDays(7),
                receiptDate ?? null,
                received ?? Constants.Income.Received,
                recurrent ?? Constants.Income.Recurrent).Value;
        }
    }
}