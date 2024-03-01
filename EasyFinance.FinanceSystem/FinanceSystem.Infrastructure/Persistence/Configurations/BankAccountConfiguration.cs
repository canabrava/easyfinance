using FinanceSystem.Domain.BankAccountAggregate;
using FinanceSystem.Domain.BankAccountAggregate.Entities;
using FinanceSystem.Domain.BankAccountAggregate.ValueObjects;
using FinanceSystem.Domain.ExpenseAggregate;
using FinanceSystem.Domain.ExpenseAggregate.ValueObjects;
using FinanceSystem.Domain.IncomeAggregate;
using FinanceSystem.Domain.IncomeAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceSystem.Infrastructure.Persistence.Configurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            ConfigureBankAccount(builder);
            ConfigureFinancialTransaction(builder);
        }

        public void ConfigureBankAccount(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccounts");

            builder.HasKey(ba => ba.Id);

            builder.Property(ba => ba.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => BankAccountId.Create(value));

            builder.Property(ba => ba.UserId)
                .IsRequired();

            builder.Property(ba => ba.AccountName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ba => ba.AccountNumber)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(ba => ba.BankCode)
                 .IsRequired()
                 .HasMaxLength(3);

            builder.Property(ba => ba.Agency)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(ba => ba.AccountType)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(ba => ba.Balance)
                .IsRequired();
        }

        public void ConfigureFinancialTransaction(EntityTypeBuilder<BankAccount> builder)
        {
            builder.OwnsMany(ba => ba.FinancialTransactions, ftb =>
            {
                ftb.ToTable("FinancialTransactions");

                ftb.WithOwner().HasForeignKey("BankAccountId");

                ftb.Property(ft => ft.Id)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => FinancialTransactionId.Create(value));

                ftb.Property(ft => ft.Amount)
                    .IsRequired();

                ftb.Property(ft => ft.CreationDate)
                    .IsRequired();

                ftb.HasOne<Income>()
                    .WithOne()
                    .HasForeignKey<FinancialTransaction>(ft => ft.IncomeId);

                ftb.Property(ft => ft.IncomeId)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => IncomeId.Create(value));

                ftb.HasOne<Expense>()
                    .WithOne()
                    .HasForeignKey<FinancialTransaction>(ft => ft.ExpenseId);

                ftb.Property(ft => ft.ExpenseId)
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => ExpenseId.Create(value));
            });
        }
    }
}
