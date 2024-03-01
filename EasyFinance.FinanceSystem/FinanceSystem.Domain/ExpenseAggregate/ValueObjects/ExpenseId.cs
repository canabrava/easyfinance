using FinanceSystem.Domain.Common;

namespace FinanceSystem.Domain.ExpenseAggregate.ValueObjects
{
    public sealed class ExpenseId : ValueObjectGuid
    {
        private ExpenseId(Guid value) : base(value)
        {
        }

        public static ExpenseId CreateUnique()
        {
            return new ExpenseId(Guid.NewGuid());
        }

        public static ExpenseId Create(Guid value)
        {
            return new ExpenseId(value);
        }
    }
}