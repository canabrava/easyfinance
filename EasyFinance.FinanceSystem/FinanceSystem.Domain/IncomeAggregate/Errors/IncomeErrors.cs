using ErrorOr;

namespace FinanceSystem.Domain.IncomeAggregate.Errors
{
    public static class IncomeErrors
    {
        public static readonly Error IncomeAlreadyReceived = Error.Validation(
            "Income.IncomeAlreadyReceived",
             "A receita já foi recebida.");

        public static readonly Error MustBeAPositiveValue = Error.Validation(
            "Income.MustBeAPositiveValue",
            "A receita tem que ser um valor positivo.");
    }
}