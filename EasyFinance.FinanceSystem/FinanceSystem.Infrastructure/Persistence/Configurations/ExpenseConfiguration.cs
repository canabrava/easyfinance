using FinanceSystem.Domain.ExpenseAggregate;
using FinanceSystem.Domain.ExpenseAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceSystem.Infrastructure.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            ConfigureExpenseTable(builder);
            ConfigurePayments(builder);

        }

        public void ConfigureExpenseTable(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses");

            builder.HasKey(e => e.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ExpenseId.Create(value));

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.Amount)
                .IsRequired();

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.DuePaymentDate);

            builder.Property(e => e.PaymentDate);

            builder.Property(e => e.Paid)
                .IsRequired();

            builder.Property(e => e.Recurrent)
                .IsRequired();
        }

        private static void ConfigurePayments(EntityTypeBuilder<Expense> builder)
        {
            builder.OwnsMany(e => e.Payments, pb =>
            {
                pb.ToTable("Payments");

                pb.WithOwner().HasForeignKey("ExpenseId");

                pb.HasKey(p => p.Id);

                pb.Property(p => p.Id)
                    .ValueGeneratedNever()
                    .HasConversion(
                    id => id.Value,
                    value => PaymentId.Create(value));

                pb.Property(p => p.PaymentDate);

                pb.Property(p => p.Amount)
                    .IsRequired();

                pb.Property(p => p.Description)
                    .HasMaxLength(50);
            });
        }
    }
}
