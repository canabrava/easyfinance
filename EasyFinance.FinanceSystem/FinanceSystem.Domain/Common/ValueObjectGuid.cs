namespace FinanceSystem.Domain.Common
{
    public abstract class ValueObjectGuid : ValueObject
    {
        public Guid Value { get; }

        protected ValueObjectGuid(Guid value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}