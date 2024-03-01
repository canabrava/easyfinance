using FinanceSystem.Domain.ExpenseAggregate.Entities;
using FinanceSystem.Tests.TestConstants;

namespace FinanceSystem.Tests.TestUtils.Expense
{
    public static class ExpenseFactory
    {
        public static Domain.ExpenseAggregate.Expense CreateExpense(
            Guid? userId = null,
            decimal? amount = null,
            string? description = null,
            DateTime? creationDate = null,
            DateTime? duePaymentDate = null,
            DateTime? paymentDate = null,
            bool? paid = null,
            bool? recurrent = null,
            List<Payment>? payments = null)
        {
            var expense = Domain.ExpenseAggregate.Expense.Create(
                userId ?? Constants.User.GetUserId(),
                amount ?? Constants.Expense.Amount,
                description ?? Constants.Expense.Description,
                creationDate ?? DateTime.Now,
                duePaymentDate ?? DateTime.Now.AddDays(7),
                paymentDate ?? null,
                paid ?? Constants.Expense.Paid,
                recurrent ?? Constants.Expense.Recurrent).Value;

            if (payments != null)
            {
                foreach (var pay in payments)
                {
                    expense.AddPayment(pay);
                }
            }

            return expense;
        }
    }
}