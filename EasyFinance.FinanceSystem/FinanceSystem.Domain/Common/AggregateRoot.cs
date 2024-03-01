namespace FinanceSystem.Domain.Common
{
    public abstract class AggregateRoot<TId> : Entity<TId>
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }

        protected readonly List<IDomainEvent> _domainEvents = new();

        public List<IDomainEvent> PopDomainEvents()
        {
            var copy = _domainEvents.ToList();
            _domainEvents.Clear();

            return copy;
        }
    }
}
