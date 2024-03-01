using ErrorOr;

namespace FinanceSystem.Domain.ExpenseAggregate.Errors
{
    public static class ExpenseErrors
    {
        public static readonly Error MustBeAPositiveValue = Error.Validation(
            "Expense.MustBeAPositiveValue",
            "A despesa tem que ser um valor positivo.");

        public static readonly Error ExpenseAlreadyPaid = Error.Validation(
            "Expense.ExpenseAlreadyPaid",
            "A despesa já foi paga.");

        public static readonly Error PaymentAmountExceedsExpenseAmount = Error.Validation(
            "Expense.PaymentAmountExceedsExpenseAmount",
            "O valor do pagamento excede o valor da despesa.");

        public static readonly Error PaymentNotFound = Error.Validation(
            "Expense.PaymentNotFound",
            "O pagamento não foi encontrado.");
    }
}