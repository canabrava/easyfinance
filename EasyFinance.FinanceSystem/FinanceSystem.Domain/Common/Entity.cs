namespace FinanceSystem.Domain.Common
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : notnull
    {
           public TId Id { get; init; }

        public override bool Equals(object? other)
        {
            if (other is null || other.GetType() != GetType())
            {
                return false;
            }

            return ((Entity<TId>)other).Id.Equals(Id);
        }

        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
        {
            return !Equals(left, right);
        }

        protected Entity(TId id) => Id = id;
    }
}
