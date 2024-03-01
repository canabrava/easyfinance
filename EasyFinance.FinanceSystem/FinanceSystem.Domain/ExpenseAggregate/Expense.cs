using ErrorOr;
using FinanceSystem.Domain.Common;
using FinanceSystem.Domain.ExpenseAggregate.Entities;
using FinanceSystem.Domain.ExpenseAggregate.Errors;
using FinanceSystem.Domain.ExpenseAggregate.ValueObjects;

namespace FinanceSystem.Domain.ExpenseAggregate
{
    public sealed class Expense : AggregateRoot<ExpenseId>
    {
        public Guid UserId { get; }
        public decimal Amount { get; private set; } = 0m;
        public string Description { get; private set; } = string.Empty;
        public DateTime CreationDate { get; }
        public DateTime? DuePaymentDate { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public bool Paid { get; private set; } = false;
        public bool Recurrent { get; private set; } = false;
        public List<Payment> Payments { get; private set; } = new List<Payment>();

        private Expense(
            ExpenseId id,
            Guid userId,
            decimal amount,
            string description,
            DateTime creationDate,
            DateTime? duePaymentDate,
            DateTime? paymentDate,
            bool paid,
            bool recurrent) : base(id)
        {
            UserId = userId;
            Amount = amount;
            Description = description;
            CreationDate = creationDate;
            DuePaymentDate = duePaymentDate;
            PaymentDate = paymentDate;
            Paid = paid;
            Recurrent = recurrent;
        }

        public static ErrorOr<Expense> Create(
            Guid userId,
            decimal amount,
            string description,
            DateTime creationDate,
            DateTime? duePaymentDate,
            DateTime? paymentDate,
            bool paid = false,
            bool recurrent = false)
        {
            if (amount <= 0)
            {
                return ExpenseErrors.MustBeAPositiveValue;
            }

            return new Expense(
                ExpenseId.CreateUnique(),
                userId,
                amount,
                description,
                creationDate,
                duePaymentDate,
                paymentDate,
                paid,
                recurrent);
        }

        public ErrorOr<Success> UpdateExpenseDetails(
            decimal amount,
            string description,
            DateTime? duePaymentDate,
            bool recurrent)
        {
            if (amount <= 0)
            {
                return ExpenseErrors.MustBeAPositiveValue;
            }

            Amount = amount;
            Description = description;
            DuePaymentDate = duePaymentDate;
            Recurrent = recurrent;

            return Result.Success;
        }

        public ErrorOr<Success> AddPayment(Payment payment)
        {
            if (Paid)
            {
                return ExpenseErrors.ExpenseAlreadyPaid;
            }

            if (Payments.Sum(p => p.Amount) + payment.Amount > Amount)
            {
                return ExpenseErrors.PaymentAmountExceedsExpenseAmount;
            }

            if (Payments.Sum(p => p.Amount) + payment.Amount == Amount)
            {
                Paid = true;
                PaymentDate = payment.PaymentDate;
            }

            Payments.Add(payment);
            return Result.Success;
        }

        public ErrorOr<Success> RemovePayment(Payment payment)
        {
            if (!Payments.Contains(payment))
            {
                return ExpenseErrors.PaymentNotFound;
            }

            if (Paid)
            {
                Paid = false;
            }

            Payments.Remove(payment);
            return Result.Success;
        }
    }
}