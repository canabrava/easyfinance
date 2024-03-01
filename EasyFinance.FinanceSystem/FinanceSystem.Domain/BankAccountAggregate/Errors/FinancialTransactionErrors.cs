using ErrorOr;

namespace FinanceSystem.Domain.BankAccountAggregate.Errors
{
    public static class FinancialTransactionErrors
    {
        public static readonly Error MustBeAPositiveValue = Error.Validation(
            "FinancialTransaction.MustBeAPositiveValue",
            "A transação financeira tem que ser um valor positivo.");

        public static readonly Error MustHaveAnExpenseOrIncome = Error.Validation(
            "FinancialTransaction.MustHaveAnExpenseOrIncome",
            "A transação financeira tem que ter um gasto ou uma renda.");

        public static readonly Error CannotHaveBothExpenseAndIncome = Error.Validation(
            "FinancialTransaction.CannotHaveBothExpenseAndIncome",
            "A transação financeira não pode ter um gasto e uma renda.");
    }
}
