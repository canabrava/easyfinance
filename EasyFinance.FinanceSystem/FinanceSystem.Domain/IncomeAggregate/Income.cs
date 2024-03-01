using ErrorOr;
using FinanceSystem.Domain.Common;
using FinanceSystem.Domain.IncomeAggregate.Errors;
using FinanceSystem.Domain.IncomeAggregate.ValueObjects;

namespace FinanceSystem.Domain.IncomeAggregate
{
    public sealed class Income : AggregateRoot<IncomeId>
    {
        public Guid UserId { get; }
        public decimal Amount { get; private set; } = 0m;
        public string Description { get; private set; } = string.Empty;
        public DateTime CreationDate { get; }
        public DateTime? ExpectedReceiptDate { get; private set; }
        public DateTime? ReceiptDate { get; private set; }
        public bool Received { get; private set; } = false;
        public bool Recurrent { get; private set; } = false;

        private Income(
            IncomeId id,
            Guid userId,
            decimal amount,
            string description,
            DateTime creationDate,
            DateTime? expectedReceiptDate,
            DateTime? receiptDate,
            bool received,
            bool recurrent) : base(id)
        {
            UserId = userId;
            Amount = amount;
            Description = description;
            CreationDate = creationDate;
            ExpectedReceiptDate = expectedReceiptDate;
            ReceiptDate = receiptDate;
            Received = received;
            Recurrent = recurrent;
        }

        public static ErrorOr<Income> Create(
            Guid userId,
            decimal amount,
            string description,
            DateTime creationDate,
            DateTime? expectedReceiptDate,
            DateTime? receiptDate,
            bool received,
            bool recurrent)
        {
            if (amount <= 0)
            {
                return IncomeErrors.MustBeAPositiveValue;
            }

            return new Income(
                IncomeId.CreateUnique(),
                userId,
                amount,
                description,
                creationDate,
                expectedReceiptDate,
                receiptDate,
                received,
                recurrent);
        }

        public ErrorOr<Success> ReceiveIncome(DateTime receiptDate)
        {
            if (Received)
            {
                return IncomeErrors.IncomeAlreadyReceived;
            }

            Received = true;
            ReceiptDate = receiptDate;

            return Result.Success;
        }

        public ErrorOr<Success> Update(Income income)
        {
            Amount = income.Amount;
            Description = income.Description;
            ExpectedReceiptDate = income.ExpectedReceiptDate;
            Recurrent = income.Recurrent;

            return Result.Success;
        }
    }
}