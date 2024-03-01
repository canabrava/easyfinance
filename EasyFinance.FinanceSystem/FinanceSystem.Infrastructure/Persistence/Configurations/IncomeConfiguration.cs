using FinanceSystem.Domain.IncomeAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FinanceSystem.Domain.IncomeAggregate.ValueObjects;

namespace FinanceSystem.Infrastructure.Persistence.Configurations
{
    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            ConfigureIncomesTable(builder);
        }

        private static void ConfigureIncomesTable(EntityTypeBuilder<Income> builder)
        {
            builder.ToTable("Incomes");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => IncomeId.Create(value));

            builder.Property(i => i.UserId)
                .IsRequired();

            builder.Property(i => i.Amount)
                .IsRequired();

            builder.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.CreationDate)
                .IsRequired();

            builder.Property(i => i.ExpectedReceiptDate);

            builder.Property(i => i.ReceiptDate);

            builder.Property(i => i.Received)
                .IsRequired();

            builder.Property(i => i.Recurrent)
                .IsRequired();
        }
    }
}
