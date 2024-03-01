using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using ErrorOr;
using FinanceSystem.Domain.Common;
using FinanceSystem.Domain.ExpenseAggregate.ValueObjects;
using FinanceSystem.Domain.IncomeAggregate.ValueObjects;
using FinanceSystem.Domain.BankAccountAggregate.Errors;

namespace FinanceSystem.Domain.BankAccountAggregate.Entities
{
    public sealed class FinancialTransaction : Entity<FinancialTransactionId>
    {
        public ExpenseId? ExpenseId { get; }
        public IncomeId? IncomeId { get; }
        public DateTime CreationDate { get; }
        public decimal Amount { get; }

        private FinancialTransaction(
            FinancialTransactionId id,
            ExpenseId? expenseId,
            IncomeId? incomeId,
            DateTime creationDate,
            decimal amount) : base(id)
        {
            ExpenseId = expenseId;
            IncomeId = incomeId;
            CreationDate = creationDate;
            Amount = amount;
        }

        public static ErrorOr<FinancialTransaction> Create(
            DateTime creationDate,
            decimal amount,
            ExpenseId? expenseId = null,
            IncomeId? incomeId = null)
        {
            if (amount <= 0)
            {
                return FinancialTransactionErrors.MustBeAPositiveValue;
            }

            if (expenseId is null && incomeId is null)
            {
                return FinancialTransactionErrors.MustHaveAnExpenseOrIncome;
            }

            if (expenseId is not null && incomeId is not null)
            {
                return FinancialTransactionErrors.CannotHaveBothExpenseAndIncome;
            }

            return new FinancialTransaction(
                FinancialTransactionId.CreateUnique(),
                expenseId,
                incomeId,
                creationDate,
                amount);
        }

        public bool IsExpense()
        {
            return ExpenseId is not null;
        }

        public bool IsIncome()
        {
            return IncomeId is not null;
        }
    }
}
