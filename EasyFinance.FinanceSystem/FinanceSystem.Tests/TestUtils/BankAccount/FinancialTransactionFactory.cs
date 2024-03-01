using FinanceSystem.Domain.ExpenseAggregate.ValueObjects;
using FinanceSystem.Domain.IncomeAggregate.ValueObjects;
using FinanceSystem.Tests.TestConstants;


namespace FinanceSystem.Tests.TestUtils.BankAccount
{
    public static class FinancialTransactionFactory
    {
        public static Domain.BankAccountAggregate.Entities.FinancialTransaction CreateFinancialTransaction(
            DateTime? date = null,
            decimal? amount = null,
            ExpenseId? expenseId = null,
            IncomeId? incomeId = null)
        {
            return Domain.BankAccountAggregate.Entities.FinancialTransaction.Create(
                date ?? DateTime.Now,
                amount ?? Constants.FinancialTransaction.Amount,
                expenseId,
                incomeId).Value;
        }
    }
}
