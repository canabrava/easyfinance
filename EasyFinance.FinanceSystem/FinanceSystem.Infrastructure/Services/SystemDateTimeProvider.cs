using FinanceSystem.Domain.Common.Interfaces;

namespace FinanceSystem.Infrastructure.Services
{
    internal class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
