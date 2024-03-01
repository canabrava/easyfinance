using FinanceSystem.Domain.Common;

namespace FinanceSystem.Domain.BankAccountAggregate.ValueObjects
{
    public sealed class FinancialTransactionId : ValueObjectGuid
    {
        private FinancialTransactionId(Guid value) : base(value)
        {
        }

        public static FinancialTransactionId CreateUnique()
        {
            return new FinancialTransactionId(Guid.NewGuid());
        }

        public static FinancialTransactionId Create(Guid value)
        {
            return new FinancialTransactionId(value);
        }
    }
}
