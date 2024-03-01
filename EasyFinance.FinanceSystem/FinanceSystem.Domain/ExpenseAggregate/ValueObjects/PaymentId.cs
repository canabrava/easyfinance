using FinanceSystem.Domain.Common;

namespace FinanceSystem.Domain.ExpenseAggregate.ValueObjects
{
    public sealed class PaymentId : ValueObjectGuid
    {
        private PaymentId(Guid value) : base(value)
        {
        }

        public static PaymentId CreateUnique()
        {
            return new PaymentId(Guid.NewGuid());
        }

        public static PaymentId Create(Guid value)
        {
            return new PaymentId(value);
        }
    }
}