using FinanceSystem.Domain.Common;

namespace FinanceSystem.Domain.BankAccountAggregate.ValueObjects
{
    public sealed class BankAccountId : ValueObjectGuid
    {
        private BankAccountId(Guid value) : base(value)
        {
        }

        public static BankAccountId CreateUnique()
        {
            return new BankAccountId(Guid.NewGuid());
        }

        public static BankAccountId Create(Guid value)
        {
            return new BankAccountId(value);
        }
    }
}
