using FinanceSystem.Tests.TestConstants;

namespace FinanceSystem.Tests.TestUtils.Expense
{
    public static class PaymentFactory
    {
        public static Domain.ExpenseAggregate.Entities.Payment CreatePayment(
            DateTime? date = null,
            decimal? amount = null,
            string? description = null)
        {
            return Domain.ExpenseAggregate.Entities.Payment.Create(
                date ?? DateTime.Now,
                amount ?? Constants.Payment.Amount,
                description ?? Constants.Payment.Description).Value;
        }
    }
}