using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.Entities;
using FinanceSystem.Domain.ExpenseAggregate;
using FinanceSystem.Domain.ExpenseAggregate.Entities;
using FinanceSystem.Domain.IncomeAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinanceSystem.Infrastructure.Persistence
{
    public class FinanceSystemDbContext : DbContext
    {
        public FinanceSystemDbContext(DbContextOptions<FinanceSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
