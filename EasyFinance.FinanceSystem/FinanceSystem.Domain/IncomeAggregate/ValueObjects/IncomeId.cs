using FinanceSystem.Domain.Common;

namespace FinanceSystem.Domain.IncomeAggregate.ValueObjects
{
    public sealed class IncomeId : ValueObjectGuid
    {
        private IncomeId(Guid value) : base(value)
        {
        }

        public static IncomeId CreateUnique()
        {
            return new IncomeId(Guid.NewGuid());
        }

        public static IncomeId Create(Guid value)
        {
            return new IncomeId(value);
        }
    }
}