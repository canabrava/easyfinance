using FinanceSystem.Application.Common.Interfaces;
using FinanceSystem.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinanceSystem.Infrastructure.Persistence.Repositories.Common
{
    public class Repository<T, TId> : IRepository<T, TId> where T : AggregateRoot<TId>
    {
        private readonly FinanceSystemDbContext _context;

        public Repository(FinanceSystemDbContext context)
        {
            _context = context;
        }
        public async Task<T> GetByIdAsync(TId id)
        {
            return await _context.Set<T>()
                .FindAsync(id);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
