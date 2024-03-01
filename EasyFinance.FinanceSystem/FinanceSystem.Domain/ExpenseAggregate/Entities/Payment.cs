using ErrorOr;
using FinanceSystem.Domain.Common;
using FinanceSystem.Domain.ExpenseAggregate.Errors;
using FinanceSystem.Domain.ExpenseAggregate.ValueObjects;

namespace FinanceSystem.Domain.ExpenseAggregate.Entities
{
    public sealed class Payment : Entity<PaymentId>
    {
        public DateTime PaymentDate { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; } = string.Empty;

        private Payment(
            PaymentId id,
            DateTime paymentDate,
            decimal amount,
            string description) : base(id)
        {
            PaymentDate = paymentDate;
            Amount = amount;
            Description = description;
        }

        public static ErrorOr<Payment> Create(DateTime paymentDate, decimal amount, string description)
        {
            if (amount <= 0)
            {
                return PaymentErrors.MustBeAPositiveValue;
            }

            return new Payment(PaymentId.CreateUnique(), paymentDate, amount, description);
        }
    }
}