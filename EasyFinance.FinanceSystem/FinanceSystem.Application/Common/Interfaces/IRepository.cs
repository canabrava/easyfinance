using FinanceSystem.Domain.Common;
using System.Linq.Expressions;

namespace FinanceSystem.Application.Common.Interfaces
{
    public interface IRepository<T, TId> where T : AggregateRoot<TId>
    {
        Task<T> GetByIdAsync(TId id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
