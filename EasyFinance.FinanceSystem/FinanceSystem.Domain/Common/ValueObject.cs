﻿namespace FinanceSystem.Domain.Common
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetEqualityComponents();

        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }

        public override bool Equals(object? other)
        {
            if (other is null || other.GetType() != GetType())
            {
                return false;
            }

            return ((ValueObject)other)
                    .GetEqualityComponents()
                    .SequenceEqual(GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }
        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !Equals(left, right);
        }
    }
}