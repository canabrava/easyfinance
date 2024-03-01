using ErrorOr;

namespace FinanceSystem.Domain.ExpenseAggregate.Errors
{
    public static class PaymentErrors
    {
        public static readonly Error MustBeAPositiveValue = Error.Validation(
            "Payment.MustBeAPositiveValue",
            "O pagamento tem que ser um valor positivo.");
    }
}